using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MusicBuzzleChecker : MonoBehaviour
{
    public GameObject[] puzzlePlats;
    List<string> savedClipsNames = new List<string>();

    public bool isMusicRunning;


    // Update is called once per frame



    private void Awake()
    {
        BuzzleMusicHandler.BuzzleRecordEvent += RecordBuzzle;
    }
    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        CLoseLights();
    }
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isMusicRunning == false)
        {
            StartCoroutine(RunBuzzleKey());
        }

    }

    IEnumerator RunBuzzleKey()
    {
        isMusicRunning = true;
        for (int i = 0; i < puzzlePlats.Length; i++)
        {
            puzzlePlats[i].GetComponent<BuzzleMusicHandler>().ExternalPlaySound();
            puzzlePlats[i].GetComponent<Light2D>().enabled = true;
            yield return new WaitForSeconds(1f);
            puzzlePlats[i].GetComponent<Light2D>().enabled = false;

        }
        isMusicRunning = false;
    }

    void RecordBuzzle(string buzzleCLipString)
    {
        savedClipsNames.Add(buzzleCLipString);
        if (savedClipsNames.Count==puzzlePlats.Length)
        {
            Debug.Log("Checking");
            CheckBuzzleResults();
            RestartData();

        }
    }

    void CheckBuzzleResults()
    {

        int counter = 0;
        for (int i = 0; i < puzzlePlats.Length; i++)
        {
 
            if (puzzlePlats[i].GetComponent<BuzzleMusicHandler>().itemPickSound == savedClipsNames[i]) 
            {
                counter++;

                if (puzzlePlats.Length== counter)
                {
                    Debug.Log("opening door");
                    deActivatePlatforms();
                    counter = 0;
                }
            }
            else
            {
                Debug.Log("respwaning");
                StartCoroutine(ActivatePlatforms());
            }
        }
    }
    void deActivatePlatforms()
    {
        for (int i = 0; i < puzzlePlats.Length; i++)
        {

            puzzlePlats[i].SetActive(false);

        }
        this.gameObject.SetActive(false);
    }
 

    void RestartData()
    {
        savedClipsNames.Clear();
        Debug.Log($"Len after clear {savedClipsNames.Count}");
    }

    IEnumerator ActivatePlatforms()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < puzzlePlats.Length; i++)
        {
           
                puzzlePlats[i].SetActive(true);
  

        }
    }

   void  CLoseLights()
    {
        for (int i = 0; i < puzzlePlats.Length; i++)
        {
            puzzlePlats[i].GetComponent<Light2D>().enabled = false ;

        }
    }
}