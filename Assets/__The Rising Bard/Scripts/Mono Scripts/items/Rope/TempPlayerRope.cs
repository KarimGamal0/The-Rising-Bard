using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerRope : MonoBehaviour
{
    private Rigidbody2D rb;
    private HingeJoint2D hj;

    [SerializeField]  float pushForce = 10f;

    bool attached = false;
    public Transform attachedTo;
    private GameObject disRegard;

    public GameObject pullSellected = null;

    private float her, ver;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hj = GetComponent<HingeJoint2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        chechInput();
    }

    private void chechInput()
    {
        her = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");
        
        if(her<0 && attached)
        {
            rb.AddRelativeForce(new Vector2(-1,0)* pushForce);
        }
        if (her > 0 && attached)
        {
            rb.AddRelativeForce(new Vector2(1, 0) * pushForce);
        }
        if (ver > 0 && attached)
        {
            //w
            Debug.Log(" w -");
            Slide(1);

        }
        if (ver < 0 && attached)
        {
            // s 
            Debug.Log(" s -");
            Slide(-1);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Deattach();
        }

    }


    private void Attach(Rigidbody2D  rigidbody2D)
    {
        rigidbody2D.gameObject.GetComponent<RopeSegment>().isPlayerAttached = true;
        hj.connectedBody = rigidbody2D;
        hj.enabled = true;
        attached = true;
        attachedTo = rigidbody2D.gameObject.transform.parent;
    }

    private void Deattach()
    {
        hj.connectedBody.gameObject.GetComponent<RopeSegment>().isPlayerAttached = false;
        hj.enabled = false;
        attached = false;
        hj.connectedBody = null;
    }

    private void Slide(int direction)
    {
        RopeSegment myconnection = hj.connectedBody.gameObject.GetComponent<RopeSegment>();
        GameObject newSegemet = null; 
        if(direction>0)
        {
            if(myconnection.connectedAbove !=null)
            {
                if(myconnection.connectedAbove.GetComponent<RopeSegment>()!=null)
                {
                    newSegemet = myconnection.connectedAbove;
                }
            }
        }
        else
        {
            if (myconnection.connecteBelow!= null)
            {
                newSegemet = myconnection.connecteBelow;
            }
        }

        if(newSegemet!=null)
        {
            transform.position = newSegemet.transform.position;
            myconnection.isPlayerAttached = false;
            newSegemet.GetComponent<RopeSegment>().isPlayerAttached = true;
            hj.connectedBody = newSegemet.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!attached && collision.gameObject.tag == "TestRope")
        {
            if((disRegard== null ||disRegard!= collision.gameObject.transform.parent.gameObject) && attachedTo != collision.gameObject.transform.parent)
            {
                Attach(collision.gameObject.GetComponent<Rigidbody2D>());
            }
        }
    }
}

