using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilitiesUiController: MonoBehaviour
{
    [SerializeField] GameObject AbilityMenuUi;
    [SerializeField] PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        AbilityMenuUi.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandelAbilitiesUiCall(InputAction.CallbackContext context)
    {
        if (context.performed )
        {
            AbilityMenuUi.SetActive(true);
        }
        else if(context.canceled)
        {
            AbilityMenuUi.SetActive(false);
        }
    }

    public void  HandelButtonClick(int value)
    {
        for (int i = 0; i < playerData.abilities.Length; i++)
        {
            playerData.abilities[i].abilityActive = (i ==value )?true: false;
        }
        
    }

}
