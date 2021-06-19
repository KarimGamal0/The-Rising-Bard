using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private float postionX;
    private Vector2 postionXStart;
    private Vector2 postionXEnd;
    private bool doorOpend = false;

    private void Awake()
    {
        postionXStart = transform.position; 
        postionXEnd = transform.position; 
    }
    private void FixedUpdate()
    {

        if (doorOpend &&Vector2.Distance(postionXStart,postionXEnd)<=4 )
        {
            postionX = transform.position.x;
            postionX += .02f;
            transform.position = new Vector3(postionX, transform.position.y, transform.position.z);
            postionXEnd = transform.position;
        }
    }

    public void OpenDoor()
    {
        doorOpend = true;
    }

}
