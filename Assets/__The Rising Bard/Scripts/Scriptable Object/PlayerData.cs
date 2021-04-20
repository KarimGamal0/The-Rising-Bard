using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{

    public Dictionary<string, bool> Abilities = new Dictionary<string, bool>
    {
        {"Dash", false},
        {"Double Jump", false},
        {"Mind Control", false},
        {"Empowered Attack", false},
        {"Time Freeze", false},
        {"Hyper Attack", false},
    };

    public float playerHP = 100;
    public float playerMana = 100;
    public float playerScore = 0;


}
