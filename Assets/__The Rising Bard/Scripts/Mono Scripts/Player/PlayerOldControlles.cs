using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOldControlles : MonoBehaviour
{

    [SerializeField] private PlayerData playerData;

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


    [Header("Dash")]
    [SerializeField] private float dashTime;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashCoolDown;


    [Header("Extra for the player")]
    [SerializeField] private float timeFreezeValue = 5.0f;

    [SerializeField] private float hangTimeSet = 0.1f;

    [SerializeField] private float jumpBufferLenght = 0.1f;

    [SerializeField] ParticleSystem dust;



    private float movementInputDirection;
    private float amountOfJumpsLeft;
    private float dashTimeLeft;
    private float lastDash;
    private float hangTime;

    private bool isGrounded;
    private bool canMove = true;
    private bool canFlip = true;
    private bool isFacingRight = true;
    private bool canJump = true;
    private bool isWalking;
    private bool isDashing;


    private int facingDirection = 1;

    private Rigidbody2D rb;
    private Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        #region Initilization data for the player


        foreach (var item in playerData.abilities)
        {
            item.abilityActive = false;
            item.abilityGained = false;
        }
        playerData.playerHP = 100;
        playerData.playerMana = 100;

        #endregion

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatAnimation();
        CheckInput();
        CheckMovementDirection();
        CheckDash();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }



    private void UpdatAnimation()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isDash", isDashing);
        //anim.SetBool("isDash", isDashing);
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");
        if(isGrounded)
        {
            hangTime = hangTimeSet;
            amountOfJumpsLeft = amountOfJumps;
        }
        else
        {
            hangTime -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump"))
        {
            
            if (amountOfJumpsLeft > 0 /* && check if he it able to duble jump */ && hangTime >0)
            {
                Jump();
            }


        }
        // time freeze 
        /*if (isTimeFreaze)
        {

            if (timeFreezeValue > 0)
            {
                timeFreezeValue -= Time.fixedDeltaTime;
            }
            else
            {
                timeFreezeValue = timeFreeze;
                isTimeFreaze = false;
                playerData.abilities[4].abilityActive = false;
            }
        }*/

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetButton("Dash"))
        {
            //  check for the mana value is able to dash or not
            if (Time.time >= (lastDash + dashCoolDown))
                AttempToDash();
        }

    }

    private void ApplyMovement()
    {
        if (canMove)
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


    public bool GetDashStatus()
    {
        return isDashing;
    }
    private void AttempToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;
    }



    private void CheckDash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                canFlip = false;
                canMove = false;
                rb.velocity = new Vector2(dashSpeed * facingDirection, 0);
                dashTimeLeft -= Time.deltaTime;
            }

            if (dashTimeLeft <= 0)
            {
                isDashing = false;
                canFlip = true;
                canMove = true;

            }
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
    public void DisableFlip()
    {
        canFlip = false;
    }


    public void EnableFlip()
    {
        canFlip = true;
    }


    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground);
    }






    // Health modificaion functions


    public void TakeDamage(float damageValue)
    {
        playerData.playerHP -= damageValue;
        Debug.Log("dameage ");
    }

    public void TakeHeal(float healValue)
    {
        playerData.playerHP += healValue;
    }



    // mana modificaion functions

    public void DecreaseMana(float decreaseValue)
    {
        playerData.playerMana -= decreaseValue;
    }

    public void IncreaseMana(float increaseValue)
    {
        playerData.playerHP += increaseValue;
    }



    // Score modificaion functions

    public void DecreaseScore(float decreaseValue)
    {
        playerData.playerScore -= decreaseValue;
    }

    public void IncreaseScore(float increaseValue)
    {
        playerData.playerScore += increaseValue;
    }



    // play partical System

    void CreateDust()
    {
        dust.Play();
    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
