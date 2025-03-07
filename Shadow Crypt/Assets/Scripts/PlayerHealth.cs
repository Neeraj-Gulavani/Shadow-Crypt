using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth=100f;
    private SpriteRenderer sr;
    public float flashDuration;
    public CinemachineImpulseSource impulseSrc;
    public Rigidbody2D rb;
    public GameObject h1,h2,h3,h4;
    public void TakeDamage(float amt) {
        Shake(0.2f);

        StartCoroutine(DamageFlash());
            if (amt>=health) {
            Die();
            health-=amt;
        } else {
            health-=amt;
        }
        DispHearts();
    }

    public void Heal(float amt) {
        if (health+amt>maxHealth) {
            health = maxHealth;
        } else {
        health+=amt;
        }
        DispHearts();
    }
    public void Die() {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        health=maxHealth;
        sr = GetComponent<SpriteRenderer>();
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
        sr.color = Color.white;
    }

    void DispHearts() {
        if (health>30) {
            h1.SetActive(true);
            h2.SetActive(true);
            h3.SetActive(true);
            h4.SetActive(true);
        } else if (health>20 && health<31) {
            h1.SetActive(true);
            h2.SetActive(true);
            h3.SetActive(true);
            h4.SetActive(false);
        } else if (health<21 && health>10) {
            h1.SetActive(true);
            h2.SetActive(true);
            h3.SetActive(false);
            h4.SetActive(false);
        } else if (health<11 && health>0) {
            h1.SetActive(true);
            h2.SetActive(false);
            h3.SetActive(false);
            h4.SetActive(false);
        }
        else if (health<=1) {
            h1.SetActive(false);
            h2.SetActive(false);
            h3.SetActive(false);
            h4.SetActive(false);
        }
    }

}
