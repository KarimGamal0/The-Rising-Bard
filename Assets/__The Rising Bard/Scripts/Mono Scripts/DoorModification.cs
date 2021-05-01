using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorModification : MonoBehaviour
{


    public void OpenDoor()
    {
        Destroy(gameObject, 1f);

    }
}
