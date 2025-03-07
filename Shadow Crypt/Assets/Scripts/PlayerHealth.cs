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
    public void TakeDamage(float amt) {
        Shake(1f);

        StartCoroutine(DamageFlash());
            if (amt>health) {
            Die();
        } else {
            health-=amt;
        }
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



}
