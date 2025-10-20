using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.InputSystem;
public class DirectionalAttack : MonoBehaviour
{
    private Animator anim;
    public TrailRenderer swordTrail;
    public SwordDamageDealer sdd;
    public bool onattack;
    public PolygonCollider2D[] colliders;
    public static float cooldown=0.8f;
    float time=0f;
    float nextfiretime=0f;
    public AudioClip[] swoosh;
    private AudioSource playerAud;
    PlayerControls controls;
    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Attack.started += ctx => Attack();
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerAud = GetComponent<AudioSource>();
        //swordTrail.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        time=Time.time;
       /* if (Input.GetMouseButtonDown(0) && time>=nextfiretime) {
            Attack();
            nextfiretime=Time.time+cooldown;
        } */
    }

    void Attack() {
        if (!(time >= nextfiretime)) return;
        anim.SetTrigger("onattack");
        nextfiretime=Time.time+cooldown;
        if (swoosh.Length == 0) return;
            int i = Random.Range(0, swoosh.Length);
            playerAud.PlayOneShot(swoosh[i]);
        StartCoroutine(EnableTrailEffect());
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

    IEnumerator EnableTrailEffect()
    {
        yield return null;
        /*
        //swordTrail.enabled = true;
        yield return new WaitForSeconds(0.3f);
        sdd.EnableHitbox();
        yield return new WaitForSeconds(0.2f);
        //swordTrail.enabled = false;
        sdd.DisableHitbox();
        */
    }
     void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
