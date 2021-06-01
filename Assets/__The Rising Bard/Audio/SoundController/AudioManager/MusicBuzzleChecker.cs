using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MusicBuzzleChecker : MonoBehaviour
{
   [SerializeField] Transform teleportArea;
   [SerializeField] GameObject playerObject;
   [SerializeField] string wrongMusicSound;
   [SerializeField] string winMusic;
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
        bool winState = false;
        for (int i = 0; i < puzzlePlats.Length; i++)
        {
 
            if (puzzlePlats[i].GetComponent<BuzzleMusicHandler>().itemPickSound == savedClipsNames[i]) 
            {
                counter++;

                if (puzzlePlats.Length== counter)
                {
                    Debug.Log("opening door");
                    AudioManager.instance.Play(winMusic);

                    deActivatePlatforms();
                    counter = 0;
                    winState = true;

                }
            }
            else
            {

                Debug.Log("respwaning");
                AudioManager.instance.Play(wrongMusicSound);
                StartCoroutine(ActivatePlatforms());
            }
            TeleportPlayer(winState);

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

    void TeleportPlayer(bool winstate)
    {
        if (winstate==true)
        {
         playerObject.transform.position = teleportArea.position;
        }
        else
        {
            playerObject.transform.position =new Vector3( this.transform.position.x+ this.GetComponent<BoxCollider2D>().size.x, this.transform.position.y,this.transform.position.z);
            //restor your pos after the key pos (I dont want to collide with the key when I get respwan)
        }
    }
}