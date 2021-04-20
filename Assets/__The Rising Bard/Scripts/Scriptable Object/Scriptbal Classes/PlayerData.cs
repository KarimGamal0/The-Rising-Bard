using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    /*
     * Dash
     * Double Jump
     * Mind Control
     * Empowered Attack
     * Time Freeze
     * Hyper Attack
     * 
     */
    public Abilities[] abilities;
    public float playerHP = 100;
    public float playerMana = 100;
    public float playerScore = 0;


}
