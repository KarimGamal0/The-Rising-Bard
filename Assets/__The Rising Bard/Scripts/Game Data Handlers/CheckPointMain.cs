using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointMain : MonoBehaviour
{
    // Start is called before the first frame update

    public levelManagerCareTaker levelManager;
    bool recordOneTime = false;



    [SerializeField] public string checkPointName = "CheckPoint";
    internal static string lastCheckPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);

        if (collision.tag == "Player")//u can do as player ,,, casting then access health or ... get so directly...
        {
            if (recordOneTime == false)
            {
                Debug.Log("recordOneTime==false");

                PlayerOldControlles player = collision.GetComponent<PlayerOldControlles>();
                if (player)
                {
                    lastCheckPoint = checkPointName;
                    var memo = player.GiveCurrentMemoToCareTaker();
                    levelManager.add(memo, $"{checkPointName}");
                    recordOneTime = true;
                    Debug.Log("checkpointsaved " + checkPointName);

                   /* Debug.Log(memo.PlayerHP);
                    Debug.Log(memo.PlayerMana);
                    Debug.Log(memo.PlayerPosition);*/
                }
            }
        }
    }



}
/*
lastCheckPoint
خد بالك 
انا عامله ستاتك 
بحيث كل مرة يحصل كول للاسكربت  
يزيد ب 1 
يعني كل تشك بوينت هيكون ليها رقم   
1 2 3 .... 
recordOneTime
للاحظ ان الاتشك بوينت هتسجل دتا البلاير مرة واحد فقط ودا منطقي 
 */