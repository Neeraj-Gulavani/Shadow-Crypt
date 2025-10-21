using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Pathfinding;
using UnityEngine.UI;
using UnityEngine.Events;
public class EnemyHealth : MonoBehaviour
{
    private AudioSource aud;
    public AudioClip deathSfx;
    private float targetFill;
    public float fillSpeed = 10f;
    public float tempHealth;
    //health bar
    
    public Image fillBar;

     void HealthBarUpdate() {
        targetFill = tempHealth / maxHealth;
    }
// ---------------------
    public float health;
    public float maxHealth=100f;
    private SpriteRenderer sr;
    public float flashDuration;
    public float knockbackForce=15f;
    public CinemachineImpulseSource impulseSrc;
    public Rigidbody2D rb;
    public Color defC;
    private AIPath aiPath;
    public float energyDrop = 10f;
    public float auraDrop = 100f;

    public UnityEvent TakeDamageAnimEvent;
    public UnityEvent DeathAnimEvent;

    private bool isDead = false;
    public void TakeDamage(float amt, Vector2 attackPosition, float kbF = 15f)
    {
        if (isDead) return;
        Shake(0.5f);
        knockbackForce = kbF;
        StartCoroutine(KnockBack(attackPosition, amt));
        StartCoroutine(DamageFlash());
        if (TakeDamageAnimEvent != null || TakeDamageAnimEvent.GetPersistentEventCount() != 0)
        {
            TakeDamageAnimEvent.Invoke();
        }

    }

    public void Heal(float amt)
    {
        if (health + amt > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += amt;
        }
        HealthBarUpdate();
    }
    public void Die()
    {
        if (deathSfx != null)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayOneShot(deathSfx);
    }
        PlayerAbilityManager pam = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerAbilityManager>(); ;
        pam.RestoreEnergy(energyDrop);
        CurrencyManager.Credit(auraDrop);
        isDead = true;

        
        if (DeathAnimEvent == null || DeathAnimEvent.GetPersistentEventCount() == 0)
        {
            DeathDestroy();
        } else
        {
            DeathAnimEvent.Invoke();

        }
        
    }
    
    public void DeathDestroy()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        aiPath = GetComponent<AIPath>();
        health=maxHealth;
        targetFill = health;
        sr = GetComponent<SpriteRenderer>();
        defC = sr.color;
        tempHealth = health;
    }

    void Shake(float intensity=1f) {
        impulseSrc.GenerateImpulseWithForce(intensity);
    }
    // Update is called once per frame
    void Update()
    {
        if (fillBar==null) return;
        fillBar.fillAmount = Mathf.Lerp(fillBar.fillAmount, targetFill, fillSpeed * Time.deltaTime);
    }

    void LateUpdate()
    {
        //fillBar.transform.rotation = Quaternion.Euler(transform.rotation.x,180,transform.rotation.z);
    }

    IEnumerator DamageFlash()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        sr.color = defC;
    }

    IEnumerator KnockBack(Vector2 attackPosition, float amt) {

        
    if (amt>=health) {
            tempHealth = 0;
        } else {
            tempHealth-=amt;
        }

    HealthBarUpdate();

        aiPath.enabled = false;
        rb.isKinematic =true;
        Vector2 knockbackDirection = (transform.position - (Vector3)attackPosition).normalized;
        float duration = 0.4f;
        float time = 0;

        while (time < duration)
        {
            
            rb.velocity = knockbackDirection * (knockbackForce * (1 - time / duration));
            time += Time.deltaTime;
            yield return null;
        }
    

        

    rb.velocity = Vector2.zero;
    rb.isKinematic=false;
    
    if (amt>=health) {
            Die();
        } else {
            health-=amt;
        }
    tempHealth = health;
    HealthBarUpdate();

    aiPath.enabled = true;
    }


}
