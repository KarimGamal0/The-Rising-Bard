using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{

    [SerializeField]
    GameObject mainmenuPanel;
    [SerializeField]
    GameObject settingsPanel; 
    [SerializeField]
    GameObject credit;

    private void Awake()
    {
        mainmenuPanel.SetActive(true);
        settingsPanel.GetComponent<Canvas>().enabled=(false);
        settingsPanel.SetActive(true);

        credit.SetActive(false);
    }
    public void Settings()
    {
        mainmenuPanel.SetActive(false);
        settingsPanel.GetComponent<Canvas>().enabled = (true);


    }
    public void Credit()
    {
        mainmenuPanel.SetActive(false);
        credit.SetActive(true);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);

    }
    public void QuitGame()
    {
        Application.Quit();
    } 

    public void callMainMemu()
    {

    }

}
