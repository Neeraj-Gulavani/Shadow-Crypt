using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class goblin_enemy : MonoBehaviour
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
    bool isattacking=false;
    
    // Start is called before the first frame update
    void Start()
    {
        aiPath = GetComponent<AIPath>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        DisableColliders();
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

        anim.SetFloat("speed",aiPath.velocity.sqrMagnitude);

         if (nextFireTime == 0)  
        {
            nextFireTime = Time.time + 0.2f;
        }
        if (player==null) return;
        float playerDistance=Vector2.Distance(transform.position,player.position);
            if (playerDistance<=shootRange) {
                if (Time.time>=nextFireTime) {
                    Slash();
                    nextFireTime = Time.time+fireRate;
                }
            } else
        {
            nextFireTime = 0f;
            }

    }
   

    void Slash(){
        isattacking=true;
        anim.SetTrigger("isattacking");
        anim.SetFloat("speed",0);
        Vector2 dir = (player.position-transform.position);
        if (Mathf.Abs(dir.x)>Mathf.Abs(dir.y)) {
            if (dir.x>0) {
                //attack right
                anim.SetInteger("attack",2);

            } else {
                //left
                anim.SetInteger("attack",3);
            }
        } else {
            if (dir.y>0) {
                //up
                anim.SetInteger("attack",1);
            } else {
                //down
                anim.SetInteger("attack",0);
            }
        }

    }

    void FlipBack()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);

    }
    void Flip() {
        transform.rotation = Quaternion.Euler(transform.rotation.x,0,transform.rotation.z);
    }
    void EndAttack() {
        isattacking=false;
    }

     public void ActivateCollider(int i) {
        
        foreach (PolygonCollider2D c in colliders) {
            c.enabled = false;
        }
        colliders[i].enabled=true;
    }

    public void DisableColliders() {
         
        foreach (PolygonCollider2D c in colliders) {
            c.enabled = false;
        }
    }



}
