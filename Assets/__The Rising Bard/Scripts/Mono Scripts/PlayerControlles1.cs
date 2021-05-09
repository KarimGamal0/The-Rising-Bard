using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlles1 : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float movementSpeed;

    [Header("Ground")]
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground;

    [Header("Jump")]
    [SerializeField] private float amountOfJumps;
    [SerializeField] private float airDragMultiplier;
    [SerializeField] private float jumpForce;





    private float movementInputDirection;
    private float amountOfJumpsLeft;

    private bool isGrounded;
    private bool canMove = true;
    private bool canFlip = true;
    private bool isFacingRight = true;
    private bool canJump = true;
    private bool isWalking ;

    private int facingDirection;

    private Rigidbody2D rb;
    private Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatAnimation();
        CheckInput();

    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }



    private void UpdatAnimation()
    {

    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || amountOfJumpsLeft > 0)
            {
                Jump();
            }

        }

    }

    private void ApplyMovement()
    {
        if (!isGrounded && movementInputDirection == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }
        else if (canMove)
        {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        }
    }

    private void Jump()
    {

        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }

    }

    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        if (Mathf.Abs(rb.velocity.x) >= 0.01f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    private void Flip()
    {
        if (canFlip)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);


    }
}
