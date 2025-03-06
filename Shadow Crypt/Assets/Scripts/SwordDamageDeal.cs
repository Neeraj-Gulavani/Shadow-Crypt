using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamageDeal : MonoBehaviour
{
    private BoxCollider2D hitbox;

    // Start is called before the first frame update
    void Start()
    {
        hitbox = GetComponent<BoxCollider2D>();
        hitbox.enabled = false;
    }

    public void EnableHitbox()
    {
        hitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }

   private void OnTriggerEnter2D(Collider2D other)
    {
        if (hitbox.enabled && other.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy: " + other.name);
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(60,transform.position);
                DisableHitbox();
                StartCoroutine(enableAgain());

            }
        }
    }

    IEnumerator enableAgain() {
        yield return new WaitForSeconds(0.18f);
        EnableHitbox();
    }
}
