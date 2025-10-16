using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Runtime.InteropServices.WindowsRuntime;
public class deathVader : MonoBehaviour
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
    public bool isattacking=false;
    
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
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
         if (state.IsName("death"))
        {
            transform.GetComponent<deathVader>().enabled = false;
        }
        bool isInAttack = state.IsName("attack");
        isattacking = isInAttack;
        aiPath.enabled = !isInAttack;
        if (isattacking) return;
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


    void Slash()
    {
        anim.SetTrigger("isattacking");
        anim.SetFloat("speed", 0);
        Vector2 dir = (player.position - transform.position);

    }
    
    public void PlayTakeDamageAnim()
    {
        anim.SetFloat("speed", 0);
        anim.SetTrigger("takedamage");
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

    public void SetIsAttackingFalse()
    {
        isattacking = false;
    }
    public void PlayDeathAnimation()
    {
        anim.SetBool("die",true);
    }

}
