using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rb;

    public float speed;
    public float detectRange;
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
        if (playerDistance<=detectRange) {

            if (playerDistance<shootRange) {
                rb.velocity=Vector2.zero;
                if (Time.time>=nextFireTime) {
                    Shoot();
                    nextFireTime = Time.time+fireRate;
                }
            } else {
                MoveToPlayer();
            }

        } else {
            //rb.velocity = Vector2.zero;
        }
    }

    void MoveToPlayer() {
        Vector2 dir = (player.position-transform.position).normalized;
        rb.velocity=dir*speed;
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
