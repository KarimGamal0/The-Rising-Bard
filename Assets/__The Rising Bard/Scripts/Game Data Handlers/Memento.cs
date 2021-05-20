using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memento : MonoBehaviour
{
    private Vector2 playerPosition;

     public Vector2 PlayerPosition
    {
        get { return playerPosition; }
    }


    private float playerHP;
    public float PlayerHP
    {
        get { return playerHP; }
    }


    private float playerMana;
    public float PlayerMana
    {
        get { return playerMana; }
    }

 


    #region Ctor to save player data in memo 
    public Memento
(
float playerHP,
float playerMana
,Vector2 playerPosition
)

    {
        this.playerHP = playerHP;
        this.playerMana = playerMana;
        this.playerPosition = playerPosition;
    }

    #endregion
    /*Constructor used to be used in orginator class ... or our player class .. he will use it to save current state 
     * in momento
    //and to give it to caretaker .... 
    player class could containt ... 
        public Memento GiveCurrentMemoToCareTaker()
      {
          return new Memento(health);
      }

    in main or whatever ... 
            Player player = new Player();
            playerData.playerHP
            player.Level = 1;
            player.Score = 100;
            player.Health = "100%";
            CareTaker careTaker = new CareTaker();
            careTaker.memoSaver = player.GiveCurrentMemoToCareTaker(player);


    */

}
