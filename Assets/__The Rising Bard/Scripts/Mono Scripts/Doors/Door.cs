using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private float postionX;
    private bool doorOpend = false;

    private void FixedUpdate()
    {
        if (doorOpend)
        {
            postionX = transform.position.x;
            postionX += .05f;
            transform.position = new Vector3(postionX, transform.position.y, transform.position.z);
        }
    }

    public void OpenDoor()
    {
        doorOpend = true;
        Destroy(gameObject, 2.5f);

    }

}
