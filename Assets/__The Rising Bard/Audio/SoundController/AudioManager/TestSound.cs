using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            AudioManager.instance.Play("step");
        }
    }
}
