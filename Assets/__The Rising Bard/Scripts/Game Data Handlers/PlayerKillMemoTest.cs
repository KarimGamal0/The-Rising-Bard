using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillMemoTest : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    public levelManagerCareTaker levelManager;
    public bool useCustomCheckPoint = false;
 


    public CheckPointMain checkPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")//u can do as player ,,, casting then access health or ... get so directly...
        {
             {

                PlayerOldControlles player = collision.GetComponent<PlayerOldControlles>();
                if (player)
                {
                    Memento memo;
                    if (useCustomCheckPoint)
                    {
                        memo = levelManager.get($"{checkPoint.checkPointName}");

                    }
                    else
                    {
                        memo = levelManager.get($"{CheckPointMain.lastCheckPoint}");
                    }
                    player.GetMementoFromCareTaker(memo);//todo event (send memo via event )

                    
                    //Debug.Log(memo.PlayerHP);
                    //Debug.Log(memo.PlayerMana);
                    //Debug.Log(memo.PlayerPosition);
          
                    //
                }
            }
        }
    }

}
