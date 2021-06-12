using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSegment : MonoBehaviour
{
    public GameObject connectedAbove, connecteBelow;
    public bool isPlayerAttached;
    // Start is called before the first frame update
    void Start()
    {
        connectedAbove = GetComponent<HingeJoint2D>().connectedBody.gameObject;
        RopeSegment aboveSegment = connectedAbove.GetComponent<RopeSegment>();
        if(aboveSegment!=null)
        {
            aboveSegment.connecteBelow = gameObject;
            float spriteButtom = connectedAbove.GetComponent<SpriteRenderer>().bounds.size.y;
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, (spriteButtom * -1 ));
        }
        else
        {
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0,0);
        }
    }


    
}
