using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamageDealer : MonoBehaviour
{

    private PolygonCollider2D hitbox;
    public GameObject EnemyHitVfx;
    public AudioClip enemyHitAudio;
    // Start is called before the first frame update
    void Start()
    {
        hitbox = GetComponent<PolygonCollider2D>();
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
                AudioSource aud = other.GetComponent<AudioSource>();
                if (aud)
                {
                    aud.PlayOneShot(enemyHitAudio);
                }
                enemy.TakeDamage(60, transform.position, 10);
                Vector2 ImpactPos = other.ClosestPoint(transform.position);
                GameObject blood = Instantiate(EnemyHitVfx, ImpactPos, Quaternion.identity);

            }
        }
  
            
    }

 
}
