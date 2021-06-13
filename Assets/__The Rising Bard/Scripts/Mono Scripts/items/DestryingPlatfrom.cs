using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestryingPlatfrom : MonoBehaviour
{
    [SerializeField] float waitTimer = 5f;

    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(WaitCoroutine(waitTimer));
        }
    }

    IEnumerator WaitCoroutine(float waitSec)
    {
        
        yield return new WaitForSeconds(waitSec);
        anim.SetBool("destroy", true);

    }
}
