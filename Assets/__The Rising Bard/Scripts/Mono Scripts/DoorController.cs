using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameEvent openDoor;
    private bool pressOpen = false;
    private bool pressEnterd = false;



    private void Update()
    {
        if (pressOpen && pressEnterd)
        {
            openDoor.Raise();
            pressOpen = false;
            pressEnterd = false;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControlles>() != null)
        {
            pressEnterd = true;
        }
    }

    public void HandleOpenDoor(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pressOpen = true;

        }
    }


}
