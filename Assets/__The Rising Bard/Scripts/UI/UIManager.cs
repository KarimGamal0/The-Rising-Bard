using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    PlayerData PD;
    [SerializeField]
    GameObject mainmenuPanel;
    [SerializeField]
    GameObject settingsPanel; 
    [SerializeField]
    GameObject credit;
    [SerializeField]
    levelManagerCareTaker levelManager;


    private void Awake()
    {
        mainmenuPanel.SetActive(true);
        //settingsPanel.GetComponent<Canvas>().enabled=(false);
        //settingsPanel.SetActive();

        //levelManager = FindObjectOfType<levelManagerCareTaker>();
        credit.SetActive(false);
    }
    public void Settings()
    {
        //mainmenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        //settingsPanel.GetComponent<Canvas>().enabled = (true);
    }
    public void Credit()
    {
        mainmenuPanel.SetActive(false);
        credit.SetActive(true);
    }
    public void NewGame()
    {
        //SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        levelManager.checkLevelData();
    }
    public void QuitGame()
    {
        Application.Quit();
    } 

    public void callMainMemu()
    {
        settingsPanel.SetActive(false);
    }

}
