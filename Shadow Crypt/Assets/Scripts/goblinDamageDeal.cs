using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblinDamageDeal : MonoBehaviour
{
    private PolygonCollider2D hitbox;
    public float dmgamt = 9f;

    // Start is called before the first frame update
    void Start()
    {
        hitbox = GetComponent<PolygonCollider2D>();
        hitbox.enabled = false;
    }

   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("yes");
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(dmgamt);
                Vector2 hitPos = other.ClosestPoint(transform.position);
                ph.ImpactFx(hitPos);

            }
        }
    }

}
