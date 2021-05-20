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
        settingsPanel.SetActive(false);
        credit.SetActive(false);
    }
    public void Settings()
    {
        mainmenuPanel.SetActive(false);
        settingsPanel.SetActive(true);

    }
    public void Credit()
    {
        mainmenuPanel.SetActive(false);
        credit.SetActive(true);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1");

    }
    public void QuitGame()
    {
        Application.Quit();
    } 

}
