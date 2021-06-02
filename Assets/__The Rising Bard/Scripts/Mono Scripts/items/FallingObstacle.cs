using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallingObstacle : MonoBehaviour
{
    public delegate void zeroParamE();
    public static event zeroParamE playerDeathE;

    [SerializeField] float timerSet;
    Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(wait());
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (rb.velocity != Vector2.zero)
            {
                playerDeathE.Invoke();
            }
        }



    }

    IEnumerator wait()
    {

        yield return new WaitForSeconds(timerSet); ;
        rb.isKinematic = false;
    }


}
