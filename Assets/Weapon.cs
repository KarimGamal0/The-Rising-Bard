using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        sprite.enabled = false;
        anim = GetComponent<Animator>();
    }

    public void OpenChest()
    {
        sprite.enabled = true;
        anim.SetBool("isOpen", true);
    }
}
