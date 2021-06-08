using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTopOfObject : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] Camera cam;

    // Update is called once per frame
    void Update()
    {
        transform.position =  cam.WorldToScreenPoint(transform.parent.position + offset);
    }
}
