using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlles : MonoBehaviour
{
    // Serialize private data
    [SerializeField] PlayerData playerData;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private BoxCollider2D boxCollider2d;

    [SerializeField] float playerSpeed = 5.0f;
    [SerializeField] float dashForced = 50.0f;
    [SerializeField] float jumpForce = 5.0f;

    [SerializeField] float timeFreezeValue = 5.0f;

    [SerializeField] float hangTime = 0.1f;

    [SerializeField] float jumpBufferLenght = 0.1f;

    [SerializeField] ParticleSystem dust;



    // Public Data



    // private Dta
    private float inputX;
    private Rigidbody2D rigidbody2d;

    private bool doubleJumpAllowed = false;
    private bool wasOnTheGround = false;

    private bool onDash = false;

    private bool isTimeFreaze = false;
    private float timeFreeze;

    private float hangCounter;
    private float extraHightCheckForGround = .1f;




    private void Awake()
    {
        #region Initilization data for the player


        foreach (var item in playerData.abilities)
        {
            item.abilityActive = false;
        }
        playerData.playerHP = 100;
        playerData.playerMana = 100;

        #endregion


        rigidbody2d = GetComponent<Rigidbody2D>();

        timeFreeze = timeFreezeValue;

    }


    void Start()
    {

    }

    // for Physics
    private void FixedUpdate()
    {
        if (onDash)
        {
            rigidbody2d.velocity = new Vector2(inputX * dashForced * playerSpeed, rigidbody2d.velocity.y);

            onDash = false;
            playerData.abilities[0].abilityActive = false;
        }
        else
        {
            rigidbody2d.velocity = new Vector2(inputX * playerSpeed, rigidbody2d.velocity.y);
        }

        // Check for last time press jump befor leave ground 
        // Manage hange time
        if (onTheGround())
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.fixedDeltaTime;
        }


        // check for time freeze 
        if (isTimeFreaze)
        {
            playerData.abilities[4].abilityActive = false;
            if (timeFreezeValue > 0)
            {
                Time.timeScale = 0.1f;
                timeFreezeValue -= Time.fixedDeltaTime;
            }
            else
            {
                timeFreezeValue = timeFreeze;
                isTimeFreaze = false;
                Time.timeScale = 1f;
            }
        }



    }


    public void HandelMove(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
        playerAnimator.SetBool("IsWalking", Convert.ToBoolean(inputX));
        //Debug.Log(inputX + " value");
        if (inputX != 0)
        {
            CreateDust();
            transform.localScale = new Vector3(inputX, transform.localScale.y, transform.localScale.z);
        }

    }



    public void HandelJump(InputAction.CallbackContext context)
    {

        if (context.performed && onTheGround())
        {
            Jump();
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.y, jumpForce);
            Debug.Log("Jump");

        }

        /*else if (doubleJumpAllowed && context.performed)
        {
            Debug.Log("doubleJump");

            Jump();
            doubleJumpAllowed = false;
            playerData.abilities[1].abilityActive = false;
        }*/

    }


    public void HandleFire(InputAction.CallbackContext context)
    {
       if(context.performed)
        {
            Debug.Log("Attack");
            playerAnimator.SetBool("IsAttacking",true);
        }
        playerAnimator.SetBool("IsAttacking", false);
    }


    public void HandleDash(InputAction.CallbackContext context)
    {
        playerData.abilities[0].abilityActive = true;
        onDash = true;
        Debug.Log("Dash");

    }
    public void HandleTimeFreeze(InputAction.CallbackContext context)
    {
        playerData.abilities[4].abilityActive = true;

        // Time Freeze
        Debug.Log("Time Freeze");
        isTimeFreaze = true;
    }
    public void HandleMindControl(InputAction.CallbackContext context)
    {
        playerData.abilities[2].abilityActive = true;

        // Mind Control
        Debug.Log("Mind Control");
    }
    public void HandleEmpoweredAttack(InputAction.CallbackContext context)
    {

        playerData.abilities[3].abilityActive = true;

        // Empowered Attack
        Debug.Log("Empowered Attack");
    }
    public void HandleHyperAttack(InputAction.CallbackContext context)
    {
        playerData.abilities[5].abilityActive = true;
        // Hyper Attack
        Debug.Log("Hyper Attack");
    }


    private bool onTheGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHightCheckForGround, groundLayer);
        Color raycolor;
        if (hit.collider != null)
            raycolor = Color.green;
        else
            raycolor = Color.red;
        return hit.collider != null;
    }

    private void Jump()
    {
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.y, jumpForce);

    }




    // Health modificaion functions


    public void TakeDamage(float damageValue)
    {
        playerData.playerHP -= damageValue;
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
}
