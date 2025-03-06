using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth=100f;

    public void TakeDamage(float amt) {
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
