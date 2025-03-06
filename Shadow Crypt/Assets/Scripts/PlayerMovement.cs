using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed=1f;
    private Animator animator;
    private float xScale, yScale;
    private float lastDir;
    public GameObject weaponH;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        xScale = transform.localScale.x;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed",movement.sqrMagnitude);
        if (movement.x!=0) {
            lastDir = MathF.Sign(movement.x);
        GetComponent<SpriteRenderer>().flipX = lastDir==-1;
        weaponH.transform.localScale = new Vector3(lastDir,weaponH.transform.localScale.y,weaponH.transform.localScale.z);
         }
    }   
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }
}
