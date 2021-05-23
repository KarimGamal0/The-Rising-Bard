using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] Slider BloodSlider;
    [SerializeField] Slider ManaSlider;

    public void SetMaxHealth(int health)
    {
        BloodSlider.maxValue = health;
        BloodSlider.value = health;
    }

    public void SetHealth(int health)
    {
        BloodSlider.value = health;
    }

    public void SetMaxMana(int mana)
    {
        ManaSlider.maxValue = mana;
        ManaSlider.value = mana;
    }

    public void SetMana(int mana)
    {
        ManaSlider.value = mana;
    }


}
