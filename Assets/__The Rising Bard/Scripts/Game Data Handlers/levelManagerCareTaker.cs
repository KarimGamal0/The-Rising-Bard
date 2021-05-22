using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class levelManagerCareTaker : MonoBehaviour
{


    [SerializeField] PlayerData PD;
    private Dictionary<string, Memento> mementoDic = new Dictionary<string, Memento>();
    public static levelManagerCareTaker instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }



    }
    public void add(Memento checkPointSnap, string key)
    {


        mementoDic.Add(key, checkPointSnap);

        Debug.Log($"last add key{key}");

    }


    public Memento get(string key)//get momento with key (name) ..you have to use add with string ... 
    {
        if (mementoDic.ContainsKey(key))
        {
            Memento val;
            mementoDic.TryGetValue(key, out val);
            return val;
        }
        else
        {
            Debug.LogError("Ensure u have check points in yout scene ");
            return null;
        }
    }


    IEnumerator OnRestart()
    {


        yield return new WaitForSeconds(1);//null;
        PD.playerHP = 100;
        PD.playerMana = 100;
        PD.playerScore = 100;
        // Destroy(playerobj);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    void RestartGame()
    {
        StartCoroutine(OnRestart());
    }

    private void OnEnable()
    {
        PlayerCombatController.playerDeathE += RestartGame;
    }

    private void OnDisable()
    {
        PlayerCombatController.playerDeathE -= RestartGame;
    }




}


//public Memento get(int index)
//{
//    return mementoList[index];
//}
//
//public void add(Memento checkPointSnap)
//{
//    mementoList.Add(checkPointSnap);
//}