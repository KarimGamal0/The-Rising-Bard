using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowToBeContinued : MonoBehaviour
{
    [SerializeField] GameObject toBeConPanel;
    bool isTinaKilled = false;
    // Start is called before the first frame update
    private void Awake()
    {
        toBeConPanel.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && isTinaKilled)
        {
            toBeConPanel.SetActive(true);
        }
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void TinaKilled()
    {
        isTinaKilled = true;
    }
}
