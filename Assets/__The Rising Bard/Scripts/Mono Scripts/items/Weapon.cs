using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Animator anim;
    [SerializeField] Transform player;
    [SerializeField] Transform telportArea;
    SpriteRenderer spriteRenderer;
    public GameEvent ShowDashPannel;
    bool isPicked = false;
    bool isChestOpend = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isPicked = true;
            ShowDashPannel.Raise();
            Debug.Log("Shwoing dash panel;");
        }
    }

    public void OpenChest()
    {
        gameObject.SetActive(true);
        isChestOpend = true;
        anim.SetBool("isOpen", true);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isPicked)
        {
            spriteRenderer.enabled = false;
            StartCoroutine(DoCheck());
        }
    }

    IEnumerator DoCheck()
    {
        yield return new WaitForSeconds(5);
        player.position = telportArea.position;
        Destroy(gameObject, 2.5f);
    }

}
