using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class deathVaderSummon : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    public float shootRange;
    public float fireRate;
    public float nextFireTime=0;
    public Transform firePt;
    public float bulletSpeed;
    private Animator anim;
    public PolygonCollider2D[] colliders;
    private AIPath aiPath;
    public bool isattacking = false;
    public float dashForce = 15f;
public float dashDuration = 0.2f;
public float damage = 20f;
public float blindDuration = 2f;
    private bool hasDamaged = false;
    
    // Start is called before the first frame update
    void Start()
    {
        aiPath = GetComponent<AIPath>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isattacking) return;
        anim.SetFloat("speed",aiPath.velocity.sqrMagnitude);

         if (nextFireTime == 0)  
        {
            nextFireTime = Time.time + 0.2f;
        }
        if (player==null) return;
        float playerDistance=Vector2.Distance(transform.position,player.position);
            if (playerDistance<=shootRange) {
            
                if (Time.time>=nextFireTime) {
                    Dash();
                    nextFireTime = Time.time+fireRate;
                }
            } else
        {
            nextFireTime = 0f;
            }

    }


    void Dash()
    {
        if (hasDamaged) return;
        if (isattacking) return;
        anim.SetFloat("speed", 0);
        StartCoroutine(DashTowardsPlayer());

    }
    IEnumerator DashTowardsPlayer()
{
    rb.velocity = Vector2.zero;
    aiPath.canMove = false;
    isattacking = true;
    yield return new WaitForSeconds(0.2f);
    aiPath.canMove = true;
    yield return new WaitForSeconds(0.00000000000000000001f);
    aiPath.canMove = false;

    Vector2 dir = (player.position - transform.position).normalized;
    // Apply dash force
    rb.AddForce(dir * dashForce, ForceMode2D.Impulse);

    yield return new WaitForSeconds(dashDuration);

    rb.velocity = Vector2.zero; // stop movement after dash

    aiPath.canMove = true;
    isattacking = false;
}

    
    public void PlayTakeDamageAnim()
    {
        anim.SetFloat("speed", 0);
        anim.SetTrigger("takedamage");
    }


    public void SetIsAttackingFalse()
    {
        isattacking = false;
    }
    public void PlayDeathAnimation()
    {
        anim.SetBool("die", true);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (hasDamaged) return;
        if (c.CompareTag("Player"))
        {
            PlayerHealth ph = c.GetComponent<PlayerHealth>();
            ph.ApplyBlindEffect();
            ph.TakeDamage(1f);
            hasDamaged = true;
            anim.SetBool("death", true);
            
        }
    }

}
