using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningFire : MonoBehaviour
{
    // Start is called before the first frame update
    public delegate void zeroParamE();
    public static event zeroParamE playerDeathE;
    public GameObject gameobj;

    public float timeToSpawn;
    private float currenttimeToSpawn;
    public bool isTimer;

    private void Start()
    {
        InvokeRepeating("SpawnObject",Random.Range(0,4), 1f);
    }
   
    public void SpawnObject()
    {
        gameobj= Instantiate(gameobj, transform.position, transform.rotation);

      
           
        
    }
   
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {

            playerDeathE.Invoke();

        }


    }

}
