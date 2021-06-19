using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DislovePlatform : MonoBehaviour
{

    Material material;
    float fadeValue = 1;
    [SerializeField] float waitSecs;
    void Start()
    {
        material = GetComponent<Renderer>().material;
        //StartCoroutine("WaitCoroutine", (waitSecs));
    }



    IEnumerator WaitCoroutine(float waitSec)
    {
        while (true)
        {
            fadeValue -= 0.05f;
            material.SetFloat("_Fade", fadeValue);
            if (fadeValue <= 0.3f)
            {
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(waitSec);
           
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(WaitCoroutine(waitSecs));
        }
    }



}

