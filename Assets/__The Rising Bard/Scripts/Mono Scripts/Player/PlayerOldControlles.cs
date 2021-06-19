using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOldControlles : MonoBehaviour
{


    public delegate void onestringdelegate(string song);
    internal static event onestringdelegate PlaySoundEvent;


    [SerializeField] private PlayerData PD;
    [SerializeField] GameEvent manaChange;

    [SerializeField] GameEvent canDashUI;
    [SerializeField] GameEvent cantDashUI;


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

    [Header("Rope Compenet")]
    [SerializeField] private Transform RopeCheck;
    [SerializeField] private LayerMask whatIsRope;
    [SerializeField] private float ropeCheckDistance;


    [Header("Extra for the player")]

    [SerializeField] private float hangTimeSet = 0.1f;

    [SerializeField] private float jumpBufferLenght = 0.1f;

    [SerializeField] private float fallDamagePerSec = 0.05f;

    [SerializeField] ParticleSystem dust;


    [Header("Music")]
    [SerializeField] private string walkSound;
    [SerializeField] private string jumpSound;


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
    private float fallDamageTimer;
    private float ropeInputDirection;


    private bool isFacingRight = true;
    private bool isWalking;
    private bool isGrounded;
    private bool canNormalJump = false;
    private bool canWallJump = false;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool isAttemptingToJump;
    private bool canMove;
    private bool canFlip;
    private bool hasWallJumped;
    private bool isTouchingLedge;
    private bool canClimbLedge = false;
    private bool ledgeDetected;
    private bool isDashing;
    private bool knockback;
    private bool lastFrameInGround = true;
    private bool isRopeCliming;
    private bool isTouchingRope;


    private int amountOfJumpsLeft;
    private int facingDirection = 1;
    private int lastWallJumpDirection = 1;

    private Vector2 ledgePosBot;
    private Vector2 ledgePos1;
    private Vector2 ledgePos2;


    private float[] attackDetails = new float[2];
    // Start is called before the first frame update
    void Start()
    {


        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
    }



    // Update is called once per frame
    void Update()
    {
        if (!PD.isPlayerStop)
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
            UpdateDashUI();
        }
    }



    private void FixedUpdate()
    {
        if (!PD.isPlayerStop)
        {
            ApplyMovement();
            CheckSurroundings();
            ApplyRopeMovement();
        }
    }



    private void UpdateAnimations()
    {

        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDash", isDashing);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetBool("isHit", knockback);
    }




    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");
        //  ropeInputDirection = Input.GetAxisRaw("Vertical");
        if (isGrounded || (amountOfJumpsLeft > 0 && isTouchingWall))
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
            jumpBufferTime = jumpBufferLenght;
        }
        else
        {
            jumpBufferTime -= Time.fixedDeltaTime;
        }
        if (Input.GetButtonDown("Jump") && amountOfJumpsLeft > 0 && PD.abilities[1].abilityGained)
        {
            NormalJump();
        }
        if (jumpBufferTime >= 0)
        {

            if (amountOfJumpsLeft >= 0 && hangTime > 0)
            {
                NormalJump();
                jumpBufferTime = 0;

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




        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            amountOfJumpsLeft--;

        }

        if (Input.GetButton("Dash"))
        {
            //  check for the mana value is able to dash or not
            // Debug.Log(PD.abilities[0].abilityGained);
            if (Time.time >= (lastDash + dashCoolDown) && PD.abilities[0].abilityGained && PD.playerMana >= PD.abilities[0].abilityCost)
            {
                AttempToDash();
                PD.playerMana -= PD.abilities[0].abilityCost;
                manaChange.Raise();
                isDashing = true;
            }
        }

    }

    void UpdateDashUI()
    {
        if (Time.time >= (lastDash + dashCoolDown) && PD.abilities[0].abilityGained && PD.playerMana >= PD.abilities[0].abilityCost)
        {
            canDashUI.Raise();
        }
        else
        {
            cantDashUI.Raise();

        }
    }

    private void AttempToDash()
    {
        //isDashing = true;
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

        isTouchingRope = Physics2D.Raycast(RopeCheck.position, transform.right, ropeCheckDistance, whatIsRope);

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

        if (Mathf.Abs(rb.velocity.x) >= 0.09f)
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

            if (!isGrounded && isTouchingWall && movementInputDirection != 0 && movementInputDirection != facingDirection)
            {
                WallJump();
            }
            else if (isGrounded || (amountOfJumpsLeft > 0 && !isTouchingWall))
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
            if (rb.velocity.y < wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
    }

    private void ApplyRopeMovement()
    {

        if (isTouchingRope && !knockback && ropeInputDirection != 0)
        {
            isRopeCliming = true;
            rb.velocity = new Vector2(rb.velocity.x, movementSpeed * ropeInputDirection);
        }
        else
        {
            isRopeCliming = false;
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
        return new Memento(transform.position);
    }

    public void GetMementoFromCareTaker(Memento memento)
    {

        transform.position = memento.PlayerPosition;

    }

    public void PlaySound(string sound)
    {
        PlaySoundEvent.Invoke(sound);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));

        Gizmos.DrawLine(RopeCheck.position, new Vector3(RopeCheck.position.x + ropeCheckDistance, RopeCheck.position.y, RopeCheck.position.z));
        // Gizmos.DrawLine(ledgeCheck.position, new Vector3(ledgeCheck.position.x + wallCheckDistance, ledgeCheck.position.y, ledgeCheck.position.z));
    }



    private void OnEnable()
    {
        HealingArea.HealPlayer += HealPlayer;

    }
    private void OnDisable()
    {
        HealingArea.HealPlayer -= HealPlayer;

    }
    void HealPlayer()
    {
        if (PD.playerHP <= PD.playerMaxHP - 1)
        {
            PD.playerHP += 1;
        }

    }
}
