using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{
    /*public string tooltipToShow;

    private void ShowMessage()
    {
        TooltipManager.OnMouseHover(tooltipToShow);
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        Debug.Log("Cursor Entering " + name + " GameObject");
        ShowMessage();
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + name + " GameObject");
    }

   /*public string message;

    private void OnMouseEnter()
    {
        TooltipManager._instance.SetAndShowToolTip(message);
    }

    private void OnMouseExit()
    {
        TooltipManager._instance.HideToolTip();
    }*/
}
