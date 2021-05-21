 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FlowerItems : MonoBehaviour
{


    private void Awake()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}

