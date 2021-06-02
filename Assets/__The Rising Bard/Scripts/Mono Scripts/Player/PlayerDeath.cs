using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public delegate void MyDelegate();
    internal static event MyDelegate playerDaeth;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerDaeth.Invoke();
        }
        
    }
}
