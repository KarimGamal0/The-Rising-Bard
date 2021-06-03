using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class levelManagerCareTaker : MonoBehaviour
{


    [SerializeField] PlayerData PD;
    private Dictionary<string, Memento> mementoDic = new Dictionary<string, Memento>();
    public static levelManagerCareTaker instance;
    public PlayerOldControlles player;


    public delegate void MyDelegate();
    internal static event MyDelegate firstCameraPostionSwitchEvent;
    //  public PlayerOldControlles player;
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
            Debug.Log("Ensure u have check points in yout scene ");
            return null;
        }
    }


    IEnumerator OnRestart()
    {
 
        yield return new WaitForSeconds(1);
        PD.restoreOrignalData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    void RestartGame()
    {
        StartCoroutine(OnRestart());
    }


    void PlayerisDead()
    {
        StartCoroutine(OnPlayerDeath());
    }


    IEnumerator OnPlayerDeath()
    {
        player.GetComponent<SpriteRenderer>().enabled = false;   //hide player from enemy then fall down . no collide you will fall
        player.GetComponent<BoxCollider2D>().enabled = false;    //hide player from enemy then fall down . no collide you will fall
        yield return new WaitForSeconds(2);//null;

        Debug.Log("OnPlayerDeath player is diead ?? event called ");


        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<BoxCollider2D>().enabled = true;
        var lastCheckPointData = get($"{CheckPointMain.lastCheckPoint}");

        firstCameraPostionSwitchEvent.Invoke();


        if (!(System.Object.ReferenceEquals(lastCheckPointData, null)) )
        {
            player.GetMementoFromCareTaker(lastCheckPointData);//restore data stored in check point

        }
        else//if no check points  restart the level and the scene 
        {
            RestartGame();
            Debug.Log("  restarting game no check points");
            //restarting game no check points
        }


    }


    private void OnEnable()
    {
        PlayerCombatController.playerDeathE += PlayerisDead;
        LoadNextLevel.loadNextLevel += LoadNextScene;
    }

    private void OnDisable()
    {
        PlayerCombatController.playerDeathE -= PlayerisDead;
        LoadNextLevel.loadNextLevel -= LoadNextScene;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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