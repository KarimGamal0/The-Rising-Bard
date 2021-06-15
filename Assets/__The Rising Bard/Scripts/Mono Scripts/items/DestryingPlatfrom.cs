using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestryingPlatfrom : MonoBehaviour
{
    [SerializeField] float waitTimer = 5f;
     Collider2D[] coliders;

    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        coliders = GetComponents<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(WaitCoroutine(waitTimer-1));
        }
    }

    IEnumerator WaitCoroutine(float waitSec)
    {
        
        yield return new WaitForSeconds(waitSec);
        foreach (var item in coliders)
        {
            item.enabled = false;
        }
        anim.SetBool("destroy", true);
        Destroy(gameObject, 1);
        Debug.Log("Destroy");


    }
}
