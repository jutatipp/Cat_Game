using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour
{
  public float speed = 5f;
    private GatherInput gatherInput;
    private new Rigidbody2D rigidbody2D;
    private Animator animator;

    private int direction = 1; // to right-hand side

    public float jumpForce = 5;

    public float rayLength;
    public LayerMask groundLayer;
    public Transform leftPoint;
    private bool grounded = false;
    
    // Start is called before the first frame update
    void Start()
    {
        gatherInput = GetComponent<GatherInput>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void SetAnimatorValues() 
    {
        animator.SetFloat("Speed", Mathf.Abs(rigidbody2D.velocity.x));
        animator.SetFloat("vSpeed", rigidbody2D.velocity.y);
        animator.SetBool("Grounded", grounded);
    }
    private void Checkstaus()
    {
        RaycastHit2D leftCheckHit = Physics2D.Raycast(leftPoint.position, Vector2.down, rayLength, groundLayer);
        grounded = leftCheckHit;
    }
    // Update is called once per frame
    void Update()
    {
        SetAnimatorValues();
        Move();
        Checkstaus();
    }
 
    private void FixedUpdate() {
        Flip();
        JumpPlayer();
    }
    private void Move(){
        rigidbody2D.velocity = new Vector2(speed * gatherInput.valueX, rigidbody2D.velocity.y);
    }
    private void Flip(){
       if(gatherInput.valueX * direction < 0) 
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            direction *= -1;
        }
    }
    private void JumpPlayer(){
        if (gatherInput.jumpInput && grounded)
        {
            rigidbody2D.velocity = new Vector2(gatherInput.valueX * speed, jumpForce);
        }
        gatherInput.jumpInput = false;
    }
}
