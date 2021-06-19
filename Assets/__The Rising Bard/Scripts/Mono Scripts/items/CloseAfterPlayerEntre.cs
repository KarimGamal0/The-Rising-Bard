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
        coliderTocheckEntrece.enabled = true;
        coliderTocheckEntrece.isTrigger = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            coliderToCoilder.enabled = true;
            coliderTocheckEntrece.enabled = false;
        }
    }
}
