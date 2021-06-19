using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObstacle : MonoBehaviour
{
    public delegate void zeroParamE();
    public static event zeroParamE playerDeathE;

    [SerializeField] float timerSetEntre;
    [SerializeField] float timerSetExit;
    float[] attackDetails = new float[2];
    private Animator anim;
    private void Awake()
    {
        anim =GetComponent<Animator>();
        attackDetails[0] = 150;
        attackDetails[0] = transform.position.x;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(waitAfterEntre());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(waitAfterExit());
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" )
        {
                playerDeathE.Invoke(); 
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Damgable"))
        {
           
            col.transform.SendMessage("Damage",attackDetails );
        }
    }

    IEnumerator waitAfterEntre()
    {
        yield return new WaitForSeconds(timerSetEntre);
        anim.SetBool("PlayerIn", true);
    }

    IEnumerator waitAfterExit()
    {
        yield return new WaitForSeconds(timerSetExit);
        anim.SetBool("PlayerIn", false);
    }


}
