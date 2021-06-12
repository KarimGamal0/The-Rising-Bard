using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope2 : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<RopeSgment> ropeSgments = new List<RopeSgment>();
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //Vector3 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public struct RopeSgment
    {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSgment(Vector2 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }
}
