using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider manaBar;
    public Mana playerMana;

    private void Start()
    {
        playerMana = GameObject.FindGameObjectWithTag("Player").GetComponent<Mana>();
        manaBar = GetComponent<Slider>();
        manaBar.maxValue = playerMana.maxMana;
        manaBar.value = playerMana.maxMana;
    }

    public void SetMana(int hp)
    {
        manaBar.value = hp;
    }
}
