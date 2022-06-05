using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textField;

    public string Text
    {
        get => textField.text;
        set => textField.text = value;
    }

}
