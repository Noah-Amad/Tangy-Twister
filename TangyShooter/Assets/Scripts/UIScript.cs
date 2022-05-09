using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public void Next_Level()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        transform.gameObject.SetActive(false);
    }

    public void Restart_Level()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
