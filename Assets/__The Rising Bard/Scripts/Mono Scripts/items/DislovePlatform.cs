using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DislovePlatform : MonoBehaviour
{

    Material material;
    float fadeValue;
    [SerializeField] float waitSecs;
    void Start()
    {
        material = GetComponent<Renderer>().material;
        StartCoroutine("WaitCoroutine", (waitSecs));
    }



    IEnumerator WaitCoroutine(float waitSec)
    {
        while (true)
        {
            if (fadeValue >= 1)
            {
                StopCoroutine("WaitCoroutine");
                StartCoroutine("WaitCoroutine2", waitSec);
            }
            fadeValue += 0.1f;

            material.SetFloat("_Fade", fadeValue);
            yield return new WaitForSeconds(waitSec);

        }


    }



    IEnumerator WaitCoroutine2(float waitSec)
    {
        while (true)
        {
            if (fadeValue <= 0)
            {
                StopCoroutine("WaitCoroutine2");
                StartCoroutine("WaitCoroutine", waitSec);
            }

            fadeValue -= 0.1f;

            material.SetFloat("_Fade", fadeValue);
            yield return new WaitForSeconds(waitSec);

        }


    }
}

