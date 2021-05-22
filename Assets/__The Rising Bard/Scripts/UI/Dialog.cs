using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    int index;

    void Start()
    {
        StartCoroutine(Type());
    }

    private void Update()
    {
        if ((textDisplay.text == setntences[index]) && (!isSentenceFinished))
        {
            isSentenceFinished = true;
            StartCoroutine(DelayButtonDisplay());
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
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
        }
    }
}
