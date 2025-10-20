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
    public bool isattacking = false;

    [Header("Summoning Settings")]
public GameObject summonPrefab;   // The enemy to summon
public int summonCount = 3;       // How many to summon
public float summonRadius = 3f;   // Distance around boss
public float summonInterval = 10f; // Time between summons
private float nextSummonTime = 10f;

    
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
        if (player == null) return;
        if (Time.time >= nextSummonTime)
{
    StartCoroutine(SummonEnemies());
    nextSummonTime = Time.time + summonInterval;
}

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
        anim.SetBool("die", true);
    }
    
   IEnumerator SummonEnemies()
    {
    Debug.Log("Summoning enemies!");

    anim.SetTrigger("issummon");
    aiPath.canMove = false;
    anim.SetFloat("speed", 0);

    yield return new WaitForSeconds(0.5f); // short delay before spawning (for animation)

    // Pick a random number of enemies to summon this time
    int actualSummonCount = Random.Range(2, summonCount + 1); // e.g., between 2 and summonCount

    for (int i = 0; i < actualSummonCount; i++)
    {
        // Distribute evenly in a circle + add a bit of randomness
        float angle = (i * Mathf.PI * 2f / actualSummonCount) + Random.Range(-0.3f, 0.3f);
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;

        // Ensure they spawn around the boss (not inside)
        Vector2 spawnPos = (Vector2)transform.position + direction * summonRadius;

        // Optional: Add a small random offset for natural spacing
        spawnPos += Random.insideUnitCircle * 0.5f;

            Instantiate(summonPrefab, spawnPos, Quaternion.identity);
        Debug.Log("Spawned summon at: " + spawnPos);

    }

    yield return new WaitForSeconds(0.5f); // small recovery delay
    aiPath.canMove = true;
}



}
