using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerAttack : MonoBehaviour
{
    PlayerControls controls;
    private Animator anim;
    public TrailRenderer swordTrail;
    public SwordDamageDeal sdd;
    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Attack.started += ctx => Attack();
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        swordTrail.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        /*
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("hi");
            Attack();
        }
        */
    }

    void Attack() {
        sdd.EnableHitbox();
        anim.SetTrigger("Attack");
        StartCoroutine(EnableTrailEffect());
    }

    IEnumerator EnableTrailEffect()
    {
        swordTrail.enabled = true;
        yield return new WaitForSeconds(0.2f);
        swordTrail.enabled = false;
        sdd.DisableHitbox();
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
