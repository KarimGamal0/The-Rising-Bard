using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{

    private Rigidbody2D rb;
    private void Start()
    {
        rb.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            rb.AddForce(-Vector2.left * 20000 * Time.deltaTime);
        }
    }

    
}
