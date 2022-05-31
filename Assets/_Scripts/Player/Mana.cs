using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public int curMana = 0;
    public int maxMana = 100;

    public ManaBar manaBar;

    void Start()
    {
        curMana = maxMana;
    }

    void Update()
    {
        if( Input.GetKeyDown( KeyCode.Mouse0 ) )
        {
            TirePlayer(25);
        }
        
         if( Input.GetKeyDown( KeyCode.Mouse1 ) )
        {
            TirePlayer(50);
        }
    }

    public void TirePlayer( int tire )
    {
        curMana -= tire;

        manaBar.SetMana( curMana );
    }
}
