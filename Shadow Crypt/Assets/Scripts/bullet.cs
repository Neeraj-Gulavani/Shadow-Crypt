using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float life=3f;
    public float damage=10f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,life);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerHealth ph =other.GetComponent<PlayerHealth>();
            if (ph!=null) {
                ph.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        if (!other.CompareTag("Enemy"))
            Destroy(gameObject);
    }
}
