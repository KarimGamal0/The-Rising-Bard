using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManagerCareTaker : MonoBehaviour
{
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
        Debug.LogError(key);
        if (mementoDic.ContainsKey(key))
        {
            Memento val;
            mementoDic.TryGetValue(key, out val);
            return val ;
        }
        else
        {
            Debug.LogError("Ensure u have check points in yout scene ");
            return new Memento(0,0,new Vector2(1,1));
        }
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