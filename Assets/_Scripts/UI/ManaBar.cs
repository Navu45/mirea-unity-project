using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider manaBar;

    public void SetMana(int hp)
    {
        manaBar.value = hp;
    }
}
