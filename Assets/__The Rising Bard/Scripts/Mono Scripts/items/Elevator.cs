using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] SpriteRenderer buttonSprite;
    [SerializeField] EdgeCollider2D edgeCollider2d;
    [SerializeField] Transform topPostion;
    [SerializeField] Transform downPostion;
    [SerializeField] float speed;

    private bool hasKey;
    private bool isElevatorDown;
    private bool isPlayerIn;
    // Start is called before the first frame update
    void Start()
    {
        buttonSprite.color = Color.red;
       // edgeCollider2d.isTrigger = true;
        isElevatorDown = false;
        isPlayerIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        StartElevator();
       
    }

    private void StartElevator()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerIn)
        {
            if (transform.position.y <= downPostion.position.y)
            {
                isElevatorDown = true;
            }
            else if (transform.position.y >= topPostion.position.y)
            {
                isElevatorDown = false;
            }
        }
        Debug.Log(isElevatorDown);
        if (isElevatorDown && isPlayerIn)
        {
            transform.position = Vector2.MoveTowards(transform.position, topPostion.position, speed * Time.deltaTime);
        }
        else if (!isElevatorDown && isPlayerIn)
        {
            transform.position = Vector2.MoveTowards(transform.position, downPostion.position, speed * Time.deltaTime);

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // hasKey = collision.gameObject.GetComponent<PlayerData>().level4Key;
            buttonSprite.color = Color.green;
            isPlayerIn = true;
        }
        else
        {
            isPlayerIn = false;
            buttonSprite.color = Color.red;
        }
    }
   
    private void OnTriggerExit2D(Collider2D collision)
    {
        //edgeCollider2d.isTrigger = true;
        buttonSprite.color = Color.red;
        isPlayerIn = false;
    }

  
}
