using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchWithOutEvent : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera firstCM;
    [SerializeField] CinemachineVirtualCamera secondCM;


    public void SwitchToFirstCam()
    {
        firstCM.Priority = 10;
        secondCM.Priority = 2;
    }
    public void SwitchToSecondCam()
    {
        firstCM.Priority = 2;
        secondCM.Priority = 10;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            SwitchToSecondCam();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            SwitchToFirstCam();
    }
}
