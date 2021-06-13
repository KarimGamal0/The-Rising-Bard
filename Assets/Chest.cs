using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator anim;
    [SerializeField] Transform player;
    [SerializeField] Transform telportArea;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OpenChest()
    {
        anim.SetBool("isOpen", true);
        StartCoroutine(DoCheck());
    }

    IEnumerator DoCheck()
    {


        yield return new WaitForSeconds(5);
        player.position = telportArea.position;

    }
}
