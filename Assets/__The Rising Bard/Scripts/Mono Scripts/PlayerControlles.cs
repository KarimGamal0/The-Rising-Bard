using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlles : MonoBehaviour
{
    // Serialize private data
    [SerializeField] PlayerData playerData;
    [SerializeField] float playerSpeed = 5.0f;
    [SerializeField] float jumpForce = 5.0f;


    // Public Data



    // private Dta
    private float inputX;
    private Rigidbody2D rigidbody2d;

    float dirX;


    bool doubleJumpAllowed = false;
    bool onTheGround = false;




    private void Awake()
    {
        playerData.Abilities["Dash"] = true;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }


    void Start()
    {

    }

    // for Physics
    private void FixedUpdate()
    {
        rigidbody2d.velocity = new Vector2(inputX * playerSpeed, rigidbody2d.velocity.y);


        // check for Jump and Duble Jump

        if (rigidbody2d.velocity.y == 0)
            onTheGround = true;
        else
            onTheGround = false;

        if (onTheGround)
            doubleJumpAllowed = true;


       
    }


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
