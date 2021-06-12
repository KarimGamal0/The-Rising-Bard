using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanForceInvertor : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerExit2D(Collider2D collision)

    {

        ConstantForce2D forceObj = collision.GetComponent<ConstantForce2D>();
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

        if (forceObj != null)
        {

            rb.velocity = Vector2.zero;

            forceObj.force = -forceObj.force;


        }
    }
}
