using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickSoundHandler : MonoBehaviour
{

    public delegate void  MyDelegate(string song);
    internal static event MyDelegate PlaySoundEvent;
    [SerializeField] string itemPickSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlaySoundEvent.Invoke(itemPickSound);
        Destroy(gameObject);
    }



}
