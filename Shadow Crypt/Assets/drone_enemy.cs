using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class drone_enemy : MonoBehaviour
{
      private Transform player;
    private Rigidbody2D rb;
    public float shootRange;
    public GameObject bullet;
    public float fireRate;
    public float nextFireTime=0;
    public Transform firePt;
    public float bulletSpeed;
    private Animator anim;
    public PolygonCollider2D[] colliders;
    private AIPath aiPath;
    
    // Start is called before the first frame update
    void Start()
    {
        aiPath = GetComponent<AIPath>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        //anim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x > transform.position.x)
        {

            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //anim.SetFloat("speed", aiPath.velocity.sqrMagnitude);

        if (nextFireTime == 0)
        {
            nextFireTime = Time.time + 0.2f;
        }
        if (player == null) return;
        float playerDistance = Vector2.Distance(transform.position, player.position);
        if (playerDistance <= shootRange)
        {
            if (Time.time >= nextFireTime)
            {
                
                aiPath.enabled=false;
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
        else
        {
            aiPath.enabled = true;
            nextFireTime = 0f;
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
