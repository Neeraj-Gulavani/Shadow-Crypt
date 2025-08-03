using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movement;
    public static float moveSpeed = 5f;
    public static bool canMove;
    private Animator animator;
    private float xScale, yScale;
    private float lastDir;
    public GameObject weaponH;
    public TrailRenderer dashTrail;
    //dash
    public float dashSpeed = 1000f, dashDuration = 0.4f, dashCooldown = 1.5f, dashCooldownTimer = 0f;
    public bool isdashing = false;
    public static bool isparry = false;


    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        xScale = transform.localScale.x;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            isparry = true;
            rb.velocity = Vector2.zero;
            canMove = false;
            Debug.Log("PARRRRY");
            animator.SetTrigger("isparry");
        }

        if (!canMove) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        if (movement.x != 0)
        {
            lastDir = MathF.Sign(movement.x);
            GetComponent<SpriteRenderer>().flipX = lastDir == -1;
            weaponH.transform.localScale = new Vector3(lastDir, weaponH.transform.localScale.y, weaponH.transform.localScale.z);
        }



        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isdashing && dashCooldownTimer <= 0)
        {
            StartCoroutine(Dash());
        }
    }
    void FixedUpdate()
    {
        if (!canMove) return;
        if (!isdashing)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        }
    }

    IEnumerator Dash()
    {

        dashTrail.enabled = true;
        //rb.isKinematic = true;
        isdashing = true;
        Vector2 dashDirection = movement.normalized;

        if (dashDirection == Vector2.zero)
        {
            dashDirection = new Vector2(lastDir, 0);
        }
        Debug.Log(dashDirection);
        float time = 0;
        while (time < dashDuration)
        {
            rb.MovePosition(rb.position + dashDirection * dashSpeed * Time.deltaTime);
            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        dashCooldownTimer = dashCooldown;
        rb.velocity = Vector2.zero;
        isdashing = false;
        //rb.isKinematic = false;
        dashTrail.enabled = false;
    }


    public void EndParry()
    {
        isparry = false;
        StartCoroutine("allowMove");
    }

    IEnumerator allowMove()
    {
        yield return new WaitForSeconds(0.3f);
        canMove = true;
    }

}
