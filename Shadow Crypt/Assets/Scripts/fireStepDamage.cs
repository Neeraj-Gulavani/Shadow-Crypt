using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireStepDamage : MonoBehaviour
{

   public float damage = 5f;
    public float damageInterval = 1f;
    private Coroutine damageCoroutine;


     void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
        {
            damageCoroutine = StartCoroutine(DamageOverTime(c.GetComponent<PlayerHealth>()));
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.CompareTag("Player") && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    private IEnumerator DamageOverTime(PlayerHealth playerHealth)
    {
        // Instant damage
        playerHealth.TakeDamage(damage);

        // Apply continuous damage
        while (true)
        {
            yield return new WaitForSeconds(damageInterval);
            playerHealth.TakeDamage(damage);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
