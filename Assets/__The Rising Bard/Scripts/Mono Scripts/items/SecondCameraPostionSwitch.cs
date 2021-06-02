using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCameraPostionSwitch : MonoBehaviour
{
    public delegate void MyDelegate();
    internal static event MyDelegate secondCameraPostionSwitchEvent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        secondCameraPostionSwitchEvent.Invoke();
    }
}
