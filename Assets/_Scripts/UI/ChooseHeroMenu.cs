using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseHeroMenu : MonoBehaviour
{
    public void StartPlay()
    {
        SceneManager.LoadScene(2);
    }
}
