using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] Slider BloodSlider;
    [SerializeField] Slider ManaSlider;
    [SerializeField] PlayerData playerData;


    private void OnEnable()
    {
        SetMaxHealth();
        SetMaxMana();
        HealingArea.updateUI += updatePlayerUI;
    }
    private void OnDisable()
    {
        HealingArea.updateUI -= updatePlayerUI;

    }

    public void SetMaxHealth()
    {
        BloodSlider.maxValue = 100;
        BloodSlider.value = playerData.playerHP;
    }

    public void ChangeHealth()
    {
        BloodSlider.value = playerData.playerHP ;
    }

    public void SetMaxMana()
    {
        ManaSlider.maxValue = 100;
        ManaSlider.value = playerData.playerMana;
    }

    public void ChangeMana()
    {
        ManaSlider.value = playerData.playerMana ;
    }
    public void updatePlayerUI()
    {
        SetMaxHealth();
        SetMaxMana();
    }



}
