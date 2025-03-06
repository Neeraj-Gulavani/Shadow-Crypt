using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    public TrailRenderer swordTrail;
    public SwordDamageDeal sdd;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        swordTrail.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("hi");
            Attack();
        }
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
}
