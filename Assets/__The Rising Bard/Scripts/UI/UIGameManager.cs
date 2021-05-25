using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIGameManager : MonoBehaviour
{
    [SerializeField]
    GameObject healthBar;
    [SerializeField]
    GameObject text;

  
    public void callMainMemu()
    {
        healthBar.SetActive(false);
        healthBar.SetActive(false);
    }
    public void cancelMainMemu()
    {
        healthBar.SetActive(true);
    }
}
