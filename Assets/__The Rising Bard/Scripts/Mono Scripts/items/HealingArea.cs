using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingArea : MonoBehaviour
{
    [SerializeField] Material defultMat;
    [SerializeField] Material matToChange;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Renderer>().material = matToChange;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            collision.GetComponent<Renderer>().material = defultMat;
        }

    }
}
