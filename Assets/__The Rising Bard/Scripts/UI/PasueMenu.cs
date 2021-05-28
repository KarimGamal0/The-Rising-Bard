using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PasueMenu : MonoBehaviour
{
    bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    [SerializeField]
    GameObject healthBar;
    [SerializeField]
    GameObject text;



    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("PRESSED");
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();

            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        healthBar.SetActive(true);
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        healthBar.SetActive(false);
    }
    public void LoadMenu()
    {
        Debug.Log("Loading Game...");
    }
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
        //Application.Quit();
    }
}
