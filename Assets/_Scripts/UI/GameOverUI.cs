using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject menu;
    private TextMeshProUGUI menuText;

    void Start()
    {
        Battlefield.OnAllEnemiesDied += ShowGameOver;
        FindObjectOfType<Player>().playerInput.actions["Pause"].performed += _ => PauseGame();
        menuText = menu.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void ShowGameOver()
    {
        menuText.text = "Game Over.";
        menu.transform.GetChild(1).gameObject.SetActive(false);
        menu.SetActive(true);
    }

    public void PauseGame()
    {
        menuText.text = "Pause.";
        menu.transform.GetChild(1).gameObject.SetActive(true);
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
