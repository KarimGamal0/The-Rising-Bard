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


    // Public Data



    // private Dta
    private float inputX;
    private Rigidbody2D rigidbody2d;

    float dirX;

    bool doubleJumpAllowed = false;
    bool onTheGround = false;
    bool onDash = false;




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
        }
        else
        {
            rigidbody2d.velocity = new Vector2(inputX * playerSpeed, rigidbody2d.velocity.y);

        }

        
        // check for Jump 

        if (rigidbody2d.velocity.y == 0)
            onTheGround = true;
        else
            onTheGround = false;


    }


    #region Input Handling Functions 


    public void HandelMove(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
        Debug.Log(inputX + " value");
    }



    public void HandelJump(InputAction.CallbackContext context)
    {
        if (context.performed && onTheGround)
        {
            Jump();
        }

        else if (doubleJumpAllowed && context.performed)
        {
            Jump();
            doubleJumpAllowed = false;
        }
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
        if (context.performed )
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
            else if(playerData.abilities[2].abilityActive)
            {
                // Mind Control
                Debug.Log("Mind Control");
            }
            else if(playerData.abilities[3].abilityActive)
            {
                // Empowered Attack
                Debug.Log("Empowered Attack");
            }
            else if(playerData.abilities[4].abilityActive)
            {
                // Time Freeze
                Debug.Log("Time Freeze");

            }
            else if(playerData.abilities[5].abilityActive)
            {
                // Hyper Attack
                Debug.Log("Hyper Attack");

            }
        }


    }


    #endregion


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










}
