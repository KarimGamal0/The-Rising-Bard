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
                    Memento memo=new Memento(new Vector2(1,2));
                    if (useCustomCheckPoint)
                    {
                        memo = levelManager.get($"{checkPoint.checkPointName}");
                        Debug.Log(memo);


                    }
                    else
                    {
                       memo = levelManager.get($"{CheckPointMain.lastCheckPoint}");
                  

                    }
                    if(memo.PlayerHP!=-1)
                    {
                        player.GetMementoFromCareTaker(memo);


                    }
                    else
                    {
                        Debug.LogError("Errrror check point not taken or check of level manager in the scene ");
                    }

                    //Debug.Log(memo.PlayerHP);
                    //Debug.Log(memo.PlayerMana);
                    //Debug.Log(memo.PlayerPosition);

                    //
                }
            }
        }
    }

}
