using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth=100f;
    public SpriteRenderer sr,sr1,sr2;
    public GameObject spawnPos;
    public float flashDuration;
    public CinemachineImpulseSource impulseSrc;
    public Image h1,h2,h3,h4;
    public Image fillBar;
    public GameObject deathui;

    public GameObject imapctVfx;
    private float targetFill;
    public float fillSpeed = 5f;

    public float lowhealth=10f;
    public GameObject lowhealthpanel;
    public void TakeDamage(float amt)
    {
        Shake(1f);
        if (PlayerMovement.isparry) return;
        StartCoroutine(DamageFlash());
        if (amt >= health)
        {
            Die();
            health -= amt;
        }
        else
        {
            health -= amt;
        }
        //DispHearts();
        HealthBarUpdate();
    }

    public void ImpactFx(Vector2 pos) {
        GameObject vfx = Instantiate(imapctVfx, pos, Quaternion.identity);
        Destroy(vfx,1f);
    }

    public void Heal(float amt) {
        if (health+amt>maxHealth) {
            health = maxHealth;
        } else {
        health+=amt;
        }
        //DispHearts();
        HealthBarUpdate();
    }
    public void Die() {
        
        deathui.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        deathui = GameObject.Find("Canvas").transform.Find("DeathUI")?.gameObject;
        deathui.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        health=maxHealth;
        targetFill = health;
        sr = GetComponent<SpriteRenderer>();
    }

    public void Shake(float intensity=1f) {
        impulseSrc.GenerateImpulseWithForce(intensity);
    }
    // Update is called once per frame
    void Update()
    {
        fillBar.fillAmount = Mathf.Lerp(fillBar.fillAmount, targetFill, fillSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Inventory.inv["health"] > 0)
            {
                Heal(15);
                Inventory.inv["health"] -= 1;
            }
        }
        if (health <= lowhealth)
        {
            lowhealthpanel.SetActive(true);
        }
        else
        {
            lowhealthpanel.SetActive(false);
        }
    }

    IEnumerator DamageFlash()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        sr.color = Color.white;
    }

    void DispHearts() {
        if (health>30) {
            h1.gameObject.SetActive(true);
            h2.gameObject.SetActive(true);
            h3.gameObject.SetActive(true);
            h4.gameObject.SetActive(true);
        } else if (health>20 && health<31) {
            h1.gameObject.SetActive(true);
            h2.gameObject.SetActive(true);
            h3.gameObject.SetActive(true);
            h4.gameObject.SetActive(false);
        } else if (health<21 && health>10) {
            h1.gameObject.SetActive(true);
            h2.gameObject.SetActive(true);
            h3.gameObject.SetActive(false);
            h4.gameObject.SetActive(false);
        } else if (health<11 && health>0) {
            h1.gameObject.SetActive(true);
            h2.gameObject.SetActive(false);
            h3.gameObject.SetActive(false);
            h4.gameObject.SetActive(false);
        }
        else if (health<=1) {
            h1.gameObject.SetActive(false);
            h2.gameObject.SetActive(false);
            h3.gameObject.SetActive(false);
            h4.gameObject.SetActive(false);
        }
    }

    void HealthBarUpdate() {
        targetFill = health / maxHealth;
        
    }
    public void setHealth(float health)
    {
        this.health = health;
        HealthBarUpdate();
    }
}
