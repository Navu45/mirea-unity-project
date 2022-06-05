using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuButtons : MonoBehaviour
{
    public void StartScene()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenInstructions(GameObject window)
    {
        window.SetActive(!window.activeSelf);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
