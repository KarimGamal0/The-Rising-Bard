using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzleMusicHandler : MonoBehaviour
{
    public delegate void MyDelegate(string song);
    internal static event MyDelegate PlaySoundEvent;

     internal static event MyDelegate BuzzleRecordEvent;


    public string itemPickSound;

    public static int numOfNotes;


    private void OnCollisionEnter2D(Collision2D collision)
    {

        numOfNotes++;
        PlaySoundEvent.Invoke(itemPickSound);
        BuzzleRecordEvent.Invoke(itemPickSound);

        gameObject.SetActive(false);

        // Debug.Log(collision.gameObject.name);

        //  Destroy(gameObject);

    }

    public void ExternalPlaySound()
    {
        PlaySoundEvent.Invoke(itemPickSound);
    }

}
