using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    
    public Transform player;
    private Rigidbody2D rb;
    public float shootRange;
    public GameObject bullet;
    public float fireRate;
    public float nextFireTime;
    public Transform firePt;
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player==null) return;
        float playerDistance=Vector2.Distance(transform.position,player.position);
            if (playerDistance<=shootRange) {
                if (Time.time>=nextFireTime) {
                    Shoot();
                    nextFireTime = Time.time+fireRate;
                }
            } 

    }
    void Shoot() {
        if (player==null || firePt==null) return;
        Vector2 dir = (player.position-firePt.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0,0,angle);
        GameObject bulletObj = Instantiate(bullet,firePt.position,rot);
        Rigidbody2D bulletRb = bulletObj.GetComponent<Rigidbody2D>();
        
        bulletRb.velocity = dir*bulletSpeed;

    }
}
