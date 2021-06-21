using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PasueMenu : MonoBehaviour
{
    bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    [SerializeField] GameObject UISound;

    [SerializeField]
    GameObject healthBar;




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
        UISound.SetActive(false);
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        healthBar.SetActive(false);

    }
    public void MusicPanel()
    {
        pauseMenuUI.SetActive(false);
        UISound.SetActive(true);
        Time.timeScale = 1f;
    }
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
        //Application.Quit();
    }


    public void BackMusicPanel()
    {
        pauseMenuUI.SetActive(true);
        UISound.SetActive(false);
    }

}
