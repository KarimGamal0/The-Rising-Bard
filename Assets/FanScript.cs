using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;
    private void Start()
    {
        rigidbody2D.GetComponent<Rigidbody2D>();
    }
    void OnTriggerStay2D(Collider c)
    {
        Debug.Log("Object is in trigger");
       

        if (c.gameObject.tag == "Player")
        {
          rigidbody2D.AddForce(-Vector2.left * 20000 * Time.deltaTime);

        }


    }
}
