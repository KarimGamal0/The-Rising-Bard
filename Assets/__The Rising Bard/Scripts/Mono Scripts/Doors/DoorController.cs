using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameEvent openDoor;
    [SerializeField] private GameObject textOpen;
    private bool playerEnterd = false;
    private bool transformZBool = false;
    private float trnsformZ ;
    private float timer = 3 ;


    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.O) && playerEnterd)
        {

            openDoor.Raise();
            playerEnterd = false;
            transformZBool = true;

        }

        if (timer > 0 && transformZBool)
        {
            trnsformZ = transform.rotation.z;
            trnsformZ += .1f;
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, trnsformZ, 0);
            timer -= Time.deltaTime;
        }
        else if(timer<0 && transformZBool)
        {
            Destroy(gameObject);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        textOpen.SetActive(true);
        if (collision.tag=="Player")
        {
            playerEnterd = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        textOpen.SetActive(false);
    }




}
