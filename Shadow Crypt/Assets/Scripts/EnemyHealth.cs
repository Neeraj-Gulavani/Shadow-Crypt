using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Pathfinding;
public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth=100f;
    private SpriteRenderer sr;
    public float flashDuration;
    public float knockbackForce=15f;
    public CinemachineImpulseSource impulseSrc;
    public Rigidbody2D rb;
    public Color defC;
    private AIPath aiPath;
    public void TakeDamage(float amt,Vector2 attackPosition) {
        Shake(0.5f);
        StartCoroutine(KnockBack(attackPosition,amt));
        StartCoroutine(DamageFlash());
        
    }

    public void Heal(float amt) {
        if (health+amt>maxHealth) {
            health = maxHealth;
        } else {
        health+=amt;
        }
    }
    public void Die() {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        aiPath = GetComponent<AIPath>();
        health=maxHealth;
        sr = GetComponent<SpriteRenderer>();
        defC = sr.color;
    }

    void Shake(float intensity=1f) {
        impulseSrc.GenerateImpulseWithForce(intensity);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DamageFlash()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        sr.color = defC;
    }

    IEnumerator KnockBack(Vector2 attackPosition, float amt) {
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
    
    if (amt>health) {
            Die();
        } else {
            health-=amt;
        }
        
    aiPath.enabled = true;
    }


}
