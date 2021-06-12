using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{


    void Start()
    {
      
    }

    void Update()
    {
        transform.position += transform.up * 6 * Time.deltaTime;
        
    }
    
}
