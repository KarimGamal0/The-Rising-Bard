using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOldControlles : MonoBehaviour
{
 
    [SerializeField] private PlayerData PD;


    [Header("Movement Compenet")]
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float jumpForce = 16f;

    [Header("Ground Compenet")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckRadius;

    [Header("Multiple Jump Compenet")]
    [SerializeField] private int amountOfJumps;

    [Header("Wall Compenet")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float wallSlideSpeed;

    [Header("Air Compenet")]
    [SerializeField] private float moveFoecOnAir;
    [SerializeField] private float airDragMultiplier;

    [Header("Jump Compenet")]
    [SerializeField] private float variableJumpHeightMultiplier = 0.5f;
    [SerializeField] private float jumpTimerSet = 0.5f;

    [Header("Wall Jump Compenet")]
    [SerializeField] private Vector2 wallHopDirection;
    [SerializeField] private Vector2 wallJumpDirection;
    [SerializeField] private float wallHopForce;
    [SerializeField] private float wallJumpForce;

    [Header("Turn Compenet")]
    [SerializeField] private float turnTimerSet = 0.1f;
    [SerializeField] private float wallJumpTimerSet = 0.5f;

    [Header("Ledge Compenet")]
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] float ledgeClimbXOffset1 = 0f;
    [SerializeField] float ledgeClimbYOffset1 = 0f;
    [SerializeField] float ledgeClimbXOffset2 = 0f;
    [SerializeField] float ledgeClimbYOffset2 = 0f;

    [Header("Dash Compenet")]
    [SerializeField] float dashTime;
    [SerializeField] float dashSpeed;
    [SerializeField] float distanceBetweenImages;
    [SerializeField] float dashCoolDown;

    [Header("knockback Compenet")]
    [SerializeField] private float knockbackDuration;
    [SerializeField] private Vector2 knockbackSpeed;


    [Header("Extra for the player")]

    [SerializeField] private float hangTimeSet = 0.1f;

    [SerializeField] private float jumpBufferLenght = 0.1f;

    [SerializeField] ParticleSystem dust;


    [Header("Music")]
    [SerializeField] private string walkSound;
    [SerializeField] private string jumpSound;




    public delegate void onestringdelegate(string song);
    internal static event onestringdelegate PlaySoundEvent;



    private Rigidbody2D rb;
    private Animator anim;

    private float movementInputDirection;
    private float jumpTimer = 0.15F;
    private float turnTimer = 0.1F;
    private float wallJumpTimer = 0.1F;
    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100f;
    private float knockbackStartTime;
    private float hangTime;
    private float jumpBufferTime;


    private bool isFacingRight = true;
    private bool isWalking;
    private bool isGrounded;
    private bool canNormalJump = false;
    private bool canWallJump = false;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool isAttemptingToJump;
    private bool checkJumpMultiplier;
    private bool canMove;
    private bool canFlip;
    private bool hasWallJumped;
    private bool isTouchingLedge;
    private bool canClimbLedge = false;
    private bool ledgeDetected;
    private bool isDashing;
    private bool knockback;


    private int amountOfJumpsLeft;
    private int facingDirection = 1;
    private int lastWallJumpDirection = 1;

    private Vector2 ledgePosBot;
    private Vector2 ledgePos1;
    private Vector2 ledgePos2;



    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in PD.abilities)
        {
            item.abilityActive = false;
            item.abilityGained = false;
        }
        PD.playerHP = 100;
        PD.playerMana = 100;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
    }



    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
        CheckJump();
       // CheckLedgeClimb();
        CheckDash();
        CheckKnockback();
    }



    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }



    private void UpdateAnimations()
    {

        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
    }




    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || (amountOfJumpsLeft > 0 && isTouchingWall))
            {
                NormalJump();
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isAttemptingToJump = true;
            }
        }

        if (Input.GetButtonDown("Horizontal") && isTouchingWall)
        {
            if (!isGrounded && movementInputDirection != facingDirection)
            {
                canMove = false;
                canFlip = false;

                turnTimer = turnTimerSet;
            }
        }

        if (turnTimer >= 0)
        {
            turnTimer -= Time.deltaTime;

            if (turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }

        if (checkJumpMultiplier && !Input.GetButton("Jump"))
        {
            checkJumpMultiplier = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }

        if (Input.GetButton("Dash"))
        {
            if (Time.time >= (lastDash + dashCoolDown))
                AttempToDash();
            Debug.Log("Dash");
        }



      /*  if (isGrounded || isTouchingWall)
        {
            hangTime = hangTimeSet;
            amountOfJumpsLeft = amountOfJumps;
        }
        else
        {
            hangTime -= Time.deltaTime;
        }*/

       /* // chek Jump Buffer
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTime = jumpBufferLenght;
        }
        else
        {
            jumpBufferTime -= Time.fixedDeltaTime;
        }

        if (jumpBufferTime >= 0)
        {
            if (hangTime > 0)
            {
                NormalJump();
                jumpBufferTime = 0;
            }
            else if (amountOfJumpsLeft >= 0 && PD.abilities[1].abilityGained && PD.playerMana >= PD.abilities[1].abilityCost)
            {
                NormalJump();
                jumpBufferTime = 0;
            }
        }*/

        if (Input.GetButtonDown("Horizontal") && isTouchingWall)
        {
            if (!isGrounded && movementInputDirection != facingDirection)
            {
                canMove = false;
                canFlip = false;

                turnTimer = turnTimerSet;
            }
        }
        if (turnTimer >= 0)
        {
            turnTimer -= Time.deltaTime;

            if (turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }



        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            amountOfJumpsLeft--;
            PlaySoundEvent.Invoke(jumpSound);

        }

        if (Input.GetButton("Dash"))
        {
            //  check for the mana value is able to dash or not
            if (Time.time >= (lastDash + dashCoolDown) && PD.abilities[0].abilityGained && PD.playerMana >= PD.abilities[0].abilityCost)
                AttempToDash();
        }

        if (isWalking)
        {
            PlaySoundEvent.Invoke(walkSound);

        }






    }



    private void AttempToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        lastImageXpos = transform.position.x;
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

                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    lastImageXpos = transform.position.x;
                }
            }

            if (dashTimeLeft <= 0 || isTouchingWall)
            {
                isDashing = false;

                canFlip = true;
                canMove = true;

            }
        }
    }



    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

       // isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, whatIsGround);


        /*if (isTouchingWall && !isTouchingLedge && !ledgeDetected)
        {
            Debug.Log("ledgeDetected  " + ledgeDetected);
            ledgeDetected = true;
            ledgePosBot = wallCheck.position;
        }*/
    }




    private void CheckIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0.01f)
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if (isTouchingWall)
        {
            checkJumpMultiplier = false;
            canWallJump = true;
        }

        if (amountOfJumpsLeft <= 0)
        {
            canNormalJump = false;
        }
        else
        {
            canNormalJump = true;
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



    private void CheckJump()
    {
        if (jumpTimer > 0)
        {
            //WallJump
            if (!isGrounded && isTouchingWall && movementInputDirection != 0 && movementInputDirection != facingDirection)
            {
                WallJump();
            }
            else if (isGrounded)
            {
                NormalJump();
            }
        }

        if (isAttemptingToJump)
        {
            jumpTimer -= Time.deltaTime;
        }


        if (wallJumpTimer > 0)
        {
            if (hasWallJumped && movementInputDirection == -lastWallJumpDirection)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                hasWallJumped = false;
            }
            else if (wallJumpTimer <= 0)
            {
                hasWallJumped = false;
            }
            else
            {
                wallJumpTimer -= Time.deltaTime;
            }
        }
    }



    private void CheckIfWallSliding()
    {
        if (isTouchingWall && movementInputDirection == facingDirection && rb.velocity.y < 0 && !canClimbLedge)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }






    private void CheckLedgeClimb()
    {
        if (ledgeDetected && !canClimbLedge)
        {
            canClimbLedge = true;
            if (isFacingRight)
            {
                ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) - ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) + ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }
            else
            {
                ledgePos1 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) + ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) - ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }
            canMove = false;
            canFlip = false;
            anim.SetBool("canClimbLedge", canClimbLedge);
        }
        if (canClimbLedge)
        {
            transform.position = ledgePos1;
            canMove = false;
            canFlip = false;
        }

    }



    public void Knockback(int direction)
    {
        knockback = true;
        knockbackStartTime = Time.time;
        rb.velocity = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
    }


    private void CheckKnockback()
    {
        if (Time.time >= knockbackDuration + knockbackStartTime && knockback)
        {
            knockback = false;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }

    public bool GetDashStatus()
    {
        return isDashing;
    }

    public void FinishLedgeClimb()
    {
        canClimbLedge = false;
        transform.position = ledgePos2;
        canMove = true;
        canFlip = true;
        ledgeDetected = false;
        anim.SetBool("canClimbLedge", canClimbLedge);

    }



    private void NormalJump()
    {
        if (canNormalJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
        }
    }





    private void WallJump()
    {
        if (canWallJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            isWallSliding = false;
            amountOfJumpsLeft = amountOfJumps;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
            turnTimer = 0;
            canMove = true;
            canFlip = true;
            hasWallJumped = true;
            wallJumpTimer = wallJumpTimerSet;
            lastWallJumpDirection = -facingDirection;

        }
    }



    private void ApplyMovement()
    {

        if (!isGrounded && !isWallSliding && movementInputDirection == 0 && !knockback)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }
        else if (canMove && !knockback)
        {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        }


        if (isWallSliding)
        {
            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
    }


    public int GetFacingDirection()
    {
        return facingDirection;
    }


    private void Flip()
    {
        if (!isWallSliding && canFlip && !knockback && !ledgeDetected)
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


    public void DisableMove()
    {
        canMove = false;
    }


    public void EnableMove()
    {
        canMove = true;
    }

    public Memento GiveCurrentMemoToCareTaker()
    {
        return new Memento(PD.playerHP, PD.playerMana, transform.position);
    }

    public void GetMementoFromCareTaker(Memento memento)
    {

        PD.playerHP = memento.PlayerHP;
        PD.playerMana = memento.PlayerMana;
        transform.position = memento.PlayerPosition;


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
       // Gizmos.DrawLine(ledgeCheck.position, new Vector3(ledgeCheck.position.x + wallCheckDistance, ledgeCheck.position.y, ledgeCheck.position.z));
    }
}
