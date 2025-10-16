using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamiteThrow : MonoBehaviour
{
   public float speed = 5f;
    public float explodeDelay = 2f;
    public GameObject explosionPrefab;

    private Vector2 moveDirection;
    Vector2 targetPos;

    void Start()
    {
        
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        targetPos = player.position;

        
        moveDirection = (targetPos - (Vector2)transform.position).normalized;

    }

    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
         transform.Rotate(0f, 0f, 360f * Time.deltaTime);
        // If dynamite has reached the target, stop moving
        if ((Vector2)transform.position == targetPos)
        {
            
            Explode();
        }
        

    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
        {

            Explode();
            PlayerHealth ph = c.GetComponent<PlayerHealth>();
            ph.TakeDamage(10f);
            
        }
    }

    void Explode()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        
        }

        Destroy(gameObject);
    }
}
