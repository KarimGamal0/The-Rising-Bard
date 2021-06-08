using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControlleSystem : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera firstCM;
    [SerializeField] CinemachineVirtualCamera secondCM;

    private void OnEnable()
    {
        levelManagerCareTaker.firstCameraPostionSwitchEvent += SwitchToSecondCam;
        FirstCameraPostionSwitch.firstCameraPostionSwitchEvent += SwitchToFirstCam;
        SecondCameraPostionSwitch.secondCameraPostionSwitchEvent += SwitchToSecondCam;
    }
    private void OnDisable()
    {
        levelManagerCareTaker.firstCameraPostionSwitchEvent -= SwitchToSecondCam;
        FirstCameraPostionSwitch.firstCameraPostionSwitchEvent -= SwitchToFirstCam;
        SecondCameraPostionSwitch.secondCameraPostionSwitchEvent -= SwitchToSecondCam;
    }
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
}
