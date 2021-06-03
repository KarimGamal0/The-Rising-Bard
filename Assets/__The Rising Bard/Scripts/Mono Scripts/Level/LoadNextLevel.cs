using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    public delegate void MyDelegate();
    internal static event MyDelegate loadNextLevel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        loadNextLevel.Invoke();
    }
}
