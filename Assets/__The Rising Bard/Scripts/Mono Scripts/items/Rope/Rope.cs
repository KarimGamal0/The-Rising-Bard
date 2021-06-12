using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] Rigidbody2D hook;
    [SerializeField] GameObject[] prefabRopeSegemnts;
    [SerializeField] int numlinks = 4 ;
    // Start is called before the first frame update
    void Start()
    {
        GenerateRope();
    }


    private void GenerateRope()
    {
        Rigidbody2D prevRigidbody = hook;
        for (int i = 0; i < numlinks; i++)
        {
            int index = Random.Range(0,prefabRopeSegemnts.Length);
            GameObject newSegment = Instantiate(prefabRopeSegemnts[index]);
            newSegment.transform.parent = transform;
            newSegment.transform.position = transform.position;
            HingeJoint2D hingeJoint2D = newSegment.GetComponent<HingeJoint2D>();
            hingeJoint2D.connectedBody = prevRigidbody;
            prevRigidbody = newSegment.GetComponent<Rigidbody2D>();

        }
    }
}
