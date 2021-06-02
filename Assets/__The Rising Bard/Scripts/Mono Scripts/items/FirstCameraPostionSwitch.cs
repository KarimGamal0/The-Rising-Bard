using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCameraPostionSwitch : MonoBehaviour
{
    public delegate void MyDelegate();
    internal static event MyDelegate firstCameraPostionSwitchEvent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO : INVOKE EVENT FOR WITCH CAMERA
        firstCameraPostionSwitchEvent.Invoke();
    }
}
