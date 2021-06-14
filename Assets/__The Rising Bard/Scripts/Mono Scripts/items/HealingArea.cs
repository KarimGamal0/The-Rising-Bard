using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingArea : MonoBehaviour
{
    [SerializeField] Material defultMat;
    [SerializeField] Material matToChange;
    [SerializeField] Color BaseColor;

    private Animator playerAnimator ;
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log( collision.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("W_Idle"));
        if (collision.tag == "Player"/*&&collision.GetComponent<PlayerOldControlles>().is*/)
        {
            playerAnimator = collision.GetComponent<Animator>();
            if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("W_Idle") &&
            playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            collision.GetComponent<Renderer>().material = matToChange;
            //todo : add heath for the player
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
