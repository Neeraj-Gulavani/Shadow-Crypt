using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;
public class Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void DestroyG()
    {
        Destroy(gameObject);
    }

    void DestroyLightningVfx()
    {

        
         Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            Vector2 playerPos = player.position;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(player.position, 20f);
            foreach (Collider2D c in colliders)
            {
                if (!c.CompareTag("Enemy")) continue;
                var ai = c.GetComponentInParent<AIPath>();
                if (ai != null && ai.enabled==false)
                {
                    ai.enabled = true;
                }
                else
                {
                    Debug.LogWarning("No AIPath found on " + c.name);
                }
            }
    Destroy(gameObject);
    }
}
