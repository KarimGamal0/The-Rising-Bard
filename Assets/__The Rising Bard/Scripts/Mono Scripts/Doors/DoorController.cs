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
        playerEnterd = false;
        isDoorOpend = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerEnterd)
        {
            openDoor.Raise();
            isDoorOpend = true;
        }
        anim.SetBool("isDoorOpen", isDoorOpend);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDoorOpend && collision.tag == "Player")

        {
            Debug.Log("Player");
            playerEnterd = true;
            textOpen.SetActive(true);
        }
       
       
    }


}
