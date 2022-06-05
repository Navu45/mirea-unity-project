using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public GameObject menu;

    void Start()
    {
        Battlefield.OnAllEnemiesDied += ShowGameOver;
    }

    private void ShowGameOver()
    {
        menu.SetActive(true);
    }
}
