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
    GameObject mainMenuPanel;
    [SerializeField]
    GameObject settingsPanel;
    [SerializeField]
    GameObject credit;
    [SerializeField]
    levelManagerCareTaker levelManager;


    private void Awake()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);

        levelManager = FindObjectOfType<levelManagerCareTaker>();
    }

    public void NewGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.DeleteAll();
        levelManager.checkLevelData();
    }
    public void LoadGame()
    {
        levelManager.checkLevelData();
        Time.timeScale = 1;
    } 

    public void Settings()
    {
        settingsPanel.SetActive(true);
    }

    public void Credit()
    {
        mainMenuPanel.SetActive(false);
        credit.SetActive(true);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    } 

    public void CallMainMenu()
    {
        settingsPanel.SetActive(false);
    }
}
