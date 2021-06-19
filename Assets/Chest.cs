using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator anim;
    public GameEvent showWeapon;
    bool isChestOpend = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OpenChest()
    {
        anim.SetBool("isOpen", true);
        isChestOpend = true;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && isChestOpend)
        {
            showWeapon.Raise();
        }
    }


}
