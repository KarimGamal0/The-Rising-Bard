using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAfterPlayerEntre : MonoBehaviour
{
    [SerializeField] Collider2D coliderToCoilder;
    [SerializeField] Collider2D coliderTocheckEntrece;
    // Start is called before the first frame update
    void Start()
    {
        coliderToCoilder.enabled = false;
    }
     
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            coliderToCoilder.enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            coliderToCoilder.enabled = true;
        }
    }
}
