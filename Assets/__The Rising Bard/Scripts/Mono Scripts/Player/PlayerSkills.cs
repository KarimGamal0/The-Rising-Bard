using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{

    [SerializeField] PlayerData playerData;
    [SerializeField] private float timeFreezeValue = 5.0f;

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

    private void CheckInput()
    {
        /* Dash
      * Double Jump
      * Mind Control
      * Empowered Attack
      * Time Freeze
      * Hyper Attack
      * */
        if (Input.GetKeyDown(KeyCode.Z) && playerData.abilities[2].abilityGained)
        {

            // Mind Control
            Debug.Log("Mind Control");

        }
        if (Input.GetKeyDown(KeyCode.X) && playerData.abilities[3].abilityGained)
        {
            //Empowered Attack
            Debug.Log("Empowered Attack");
        }
        if (Input.GetKeyDown(KeyCode.C) && playerData.abilities[4].abilityGained)
        {
            // Time Freeze
             Debug.Log("Time Freeze");

            /*if (isTimeFreaze)
            {

                if (timeFreezeValue > 0)
                {
                    timeFreezeValue -= Time.DeltaTime;
                }
                else
                {
                    timeFreezeValue = timeFreeze;
                    isTimeFreaze = false;
                    playerData.abilities[4].abilityActive = false;
                }
            }*/
        }
        if (Input.GetKeyDown(KeyCode.V) && playerData.abilities[5].abilityGained)
        {
            //Hyper Attack
            Debug.Log("Hyper Attack");
        }


    }

    private void UpdatAnimation()
    {

        //anim.SetBool("isDash", isDashing);
        //anim.SetBool("isDash", isDashing);
    }




}
