using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlles : MonoBehaviour
{
    // Serialize private data
    [SerializeField] PlayerData playerData;

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
    private bool onTheGround = false;
    private bool wasOnTheGround = false;

    private bool onDash = false;

    private bool isTimeFreaze = false;
    private float timeFreeze;

    private float hangCounter;




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
            rigidbody2d.velocity = new Vector2(inputX * dashForced * playerSpeed, rigidbody2d.velocity.y)
                * Time.fixedDeltaTime * 10 * playerSpeed * (1/Time.timeScale);
            onDash = false;
            playerData.abilities[0].abilityActive = false;
        }
        else
        {
            rigidbody2d.velocity = new Vector2(inputX * playerSpeed, rigidbody2d.velocity.y)
                * Time.fixedDeltaTime*10* playerSpeed * (1 / Time.timeScale); 

        }

        // Check for last time press jump befor leave ground 
        // Manage hange time
        if(onTheGround)
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.fixedDeltaTime;
        }


       

        // check for Jump 

        if (rigidbody2d.velocity.y == 0)
            onTheGround = true;
        else
            onTheGround = false;


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


    #region Input Handling Functions 


    public void HandelMove(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
        Debug.Log(inputX + " value");
        if (inputX != 0)
        {
            CreateDust();
            transform.localScale = new Vector3(inputX, transform.localScale.y, transform.localScale.z);
        }
        
    }



    public void HandelJump(InputAction.CallbackContext context)
    {
        /*if(context.performed)
        {
            jumpBufferCounter = jumpBufferLenght;
        }
        else
        {
            jumpBufferCounter -= Time.fixedDeltaTime;
        }*/
        if ( context.performed&& hangCounter>0f)
        {
            Jump();
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.y, jumpForce)
            * Time.fixedDeltaTime * 10 * playerSpeed * (1 / Time.timeScale);

        }

        else if (doubleJumpAllowed && context.performed)
        {
            Jump();
            doubleJumpAllowed = false;
            playerData.abilities[1].abilityActive = false;
        }
        // sa=mall jump need to be fixed
        /*else if(context.canceled && rigidbody2d.velocity.y>0)
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.y, jumpForce * 0.5f)
           * Time.fixedDeltaTime * 10 * playerSpeed * (1 / Time.timeScale);
        }*/
    }

    public void HandelFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Attack");
        }


    }


    public void HandleAbility(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (playerData.abilities[0].abilityActive)
            {
                // Dash 
                Debug.Log("Dash");
                onDash = true;
            }
            else if (playerData.abilities[1].abilityActive)
            {
                // Double Jump 
                Debug.Log("Double Jump");
                doubleJumpAllowed = true;
            }
            else if (playerData.abilities[2].abilityActive)
            {
                // Mind Control
                Debug.Log("Mind Control");
            }
            else if (playerData.abilities[3].abilityActive)
            {
                // Empowered Attack
                Debug.Log("Empowered Attack");
            }
            else if (playerData.abilities[4].abilityActive)
            {
                // Time Freeze
                Debug.Log("Time Freeze");
                isTimeFreaze = true;

            }
            else if (playerData.abilities[5].abilityActive)
            {
                // Hyper Attack
                Debug.Log("Hyper Attack");

            }
        }


    }


    #endregion


    private void Jump()
    {
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.y, jumpForce)
             * Time.fixedDeltaTime *10 * playerSpeed  * (1 / Time.timeScale);
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
