using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporting : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform telportArea;
 

   

    IEnumerator DoCheck()
    {


        yield return new WaitForSeconds(3);
        player.position = telportArea.position;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        StartCoroutine(DoCheck());
    }
}
