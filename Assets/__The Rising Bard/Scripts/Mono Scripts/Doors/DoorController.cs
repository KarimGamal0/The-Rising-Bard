using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameEvent openDoor;
    [SerializeField] private GameObject textOpen;
    private bool playerEnterd = false;
    private bool isDoorOpend = false;
    private Animator anim;

    private void Awake()
    {
        textOpen.SetActive(false);
        anim = gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Debug.Log(Input.GetKeyDown(KeyCode.O)+ " o ");
        if (Input.GetKeyDown(KeyCode.O) && playerEnterd)
        {
            openDoor.Raise();
            isDoorOpend = true;
        }
        anim.SetBool("isDoorOpen",isDoorOpend);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isDoorOpend)
        textOpen.SetActive(true);
        if (collision.tag=="Player")
        {
            Debug.Log("Player");
            playerEnterd = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        textOpen.SetActive(false);
    }
}
