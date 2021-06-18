using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class Dialog : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textDisplay;

    [SerializeField]
    string[] setntences;

    [SerializeField]
    float typingSpeed;

    [SerializeField]
    float delayButtonDuration = 5.0f;

    bool isSentenceFinished;

    [SerializeField]
    GameObject continueButton;

    [SerializeField]
    GameObject palnel;



    int index;
    Image image1;
    [SerializeField]
    Sprite sprite1;
    [SerializeField]
    Sprite sprite2;
    [SerializeField]
    Sprite sprite3;

    void Start()
    {
        StartCoroutine(Type());
        image1 = palnel.GetComponent<Image>();
    }

    private void Update()
    {
        if ((textDisplay.text == setntences[index]) && (!isSentenceFinished))
        {
            isSentenceFinished = true;
            StartCoroutine(DelayButtonDisplay());
        }

        if (index == 0)
        {
            image1.sprite = sprite1;
        }
        else if (index == 8)
        {
            image1.sprite = sprite2;
        }
        else if (index == 12)
        {
            image1.sprite = sprite3;
        }

    }

    IEnumerator DelayButtonDisplay()
    {
        yield return new WaitForSeconds(delayButtonDuration);
        continueButton.SetActive(true);
    }

    IEnumerator Type()
    {
        foreach (char letter in setntences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        isSentenceFinished = false;

        if (index < setntences.Length - 1)
        {
            index++;
            Debug.Log("sssss");
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else if (index >= setntences.Length - 1)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
        }
    }


}
