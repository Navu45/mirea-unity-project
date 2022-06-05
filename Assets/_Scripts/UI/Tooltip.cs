using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea(maxLines:10, minLines: 6)]
    public string tooltipToShow;
    public TooltipText tooltipText;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        tooltipText.gameObject.SetActive(true);
        tooltipText.Text = tooltipToShow;
        
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        tooltipText.gameObject.SetActive(false);
    }
}
