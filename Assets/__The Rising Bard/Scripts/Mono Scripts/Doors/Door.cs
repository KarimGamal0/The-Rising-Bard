using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private float postionX;
    private bool doorOpend = false;

    private void FixedUpdate()
    {
        if (doorOpend && postionX<=0.95)
        {
            postionX = transform.position.x;
            postionX += .02f;
            transform.position = new Vector3(postionX, transform.position.y, transform.position.z);
            
        }
    }

    public void OpenDoor()
    {
        doorOpend = true;
    }

}
