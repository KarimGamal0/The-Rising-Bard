using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Abilites : MonoBehaviour
{

    [Header("Ability 1")]
    public Image abilityImage1;
    public float cooldown = 5;
    bool isCooldown = false;
    public KeyCode ability1;

    void Start()
    {
        abilityImage1.fillAmount = 0;
        
    }

    // Update is called once per frame
    void Update()
    {

        Ability1();
    }
    void Ability1()
    {
        if (Input.GetKey(ability1) && isCooldown == false)
        {
            isCooldown = true;
            abilityImage1.fillAmount = 1;

        }
        if (isCooldown)
        {
            abilityImage1.fillAmount -= 1 / cooldown * Time.deltaTime;
            if (abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 0;
                isCooldown = false;
            }
        }

    }
}
