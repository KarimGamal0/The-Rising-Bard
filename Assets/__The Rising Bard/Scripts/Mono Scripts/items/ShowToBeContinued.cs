using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowToBeContinued : MonoBehaviour
{
    [SerializeField] GameObject toBeConPanel;
    // Start is called before the first frame update
    private void Awake()
    {
        toBeConPanel.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" )
        {
            toBeConPanel.SetActive(true);
        }
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
