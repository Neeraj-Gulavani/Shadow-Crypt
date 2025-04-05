using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Barrel_Enemy : MonoBehaviour
{
     private Transform player;
    private Rigidbody2D rb;
    public float shootRange;
    public float fireRate;
    public float nextFireTime;
    private Animator anim;
    public GameObject explosionVfx;
    private AIPath aiPath;
    bool isattacking=false;


    public float explosionRadius = 5f;
    public float explosionForce = 50f;
    public float damage = 40f;
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
         if (player.position.x > transform.position.x)
        {
            
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
         anim.SetFloat("speed",aiPath.velocity.sqrMagnitude);
        if (player==null) return;
        float playerDistance=Vector2.Distance(transform.position,player.position);
            if (playerDistance<=shootRange) {
                if (Time.time>=nextFireTime) {
                    StartCoroutine("Explode");
                    nextFireTime = Time.time+fireRate;
                }
            } 

    }
   

    IEnumerator Explode(){
        isattacking=true;
        anim.SetTrigger("isattacking");
        anim.SetFloat("speed",0);
        yield return new WaitForSeconds(0.8f);
        float dist = Vector2.Distance(transform.position, player.transform.position);
        if (dist>3f) {
            yield break;

        }
        anim.SetTrigger("isattacking");
        aiPath.canMove=false;
        yield return new WaitForSeconds(1f);
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
         Instantiate(explosionVfx,transform.position,Quaternion.identity);
         foreach (Collider2D obj in objects)
        {
            PlayerHealth ph = obj.GetComponent<PlayerHealth>();
            float distance = Vector2.Distance(transform.position, obj.transform.position);
           
            if (ph != null)
            {
                Instantiate(explosionVfx,transform.position,Quaternion.identity);

                ph.Shake(2f);
                float damageAmount = Mathf.Lerp(damage, 0, distance / explosionRadius);
                ph.TakeDamage(damageAmount);
                
            }

            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = obj.transform.position - transform.position;
                float force = Mathf.Lerp(explosionForce, 0, distance / explosionRadius);
                rb.AddForce(direction.normalized * force);
                Destroy(gameObject,0.7f);
                
            }
       
    }
    
    }

    void ObjectDestroy() {
        Destroy(gameObject);
    }
    void FlipBack() {
        transform.rotation = Quaternion.Euler(transform.rotation.x,180,transform.rotation.z);
    }
    void Flip() {
        transform.rotation = Quaternion.Euler(transform.rotation.x,0,transform.rotation.z);
    }
    void EndAttack() {
        isattacking=false;
    }

   

}
