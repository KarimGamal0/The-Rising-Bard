using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControlleSystem : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera firstCM;
    [SerializeField] CinemachineVirtualCamera secondCM;

    public void SwitchToFirstCam()
    {
        firstCM.Priority = 10;
        secondCM.Priority = 1;
    }
    public void SwitchToSecondCam()
    {
        firstCM.Priority = 1;
        secondCM.Priority = 10;
    }
}
