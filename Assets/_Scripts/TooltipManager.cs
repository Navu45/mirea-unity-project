using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    public TextMeshProUGUI tooltipText;
    public RectTransform tooltipWindow;
   /* public static Action<string> OnMouseHover;
    public static Action OnMouseLose;*/
    [SerializeField]
    private Camera uiCamera;

    /*private void OnEnable()
    {
        OnMouseHover += ShowToolTip;
        OnMouseLose += HideToolTip;
    }

    private void OnDisable()
    {
        OnMouseHover -= ShowToolTip;
        OnMouseLose -= HideToolTip;
    }*/

    private void Awake()
    {
        tooltipWindow = transform.Find("").GetComponent<RectTransform>();
        tooltipText = transform.Find("").GetComponent<TextMeshProUGUI>();
    }
    private void ShowToolTip(string text)
    {
        float textPaddingSize = 4f;
        tooltipText.text = text;
        Vector2 bgSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2f, tooltipText.preferredHeight + textPaddingSize * 2f);
        tooltipWindow.gameObject.SetActive(true);
        tooltipWindow.sizeDelta = bgSize;
    }

    private void HideToolTip()
    {
        tooltipWindow.gameObject.SetActive(false);
    }

    private void Start()
    {
        HideToolTip();
    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.localPosition = localPoint;
    }
}







/*public class TooltipManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
 + tooltipWindow.sizeDelta.x * 2
{
    public TextMeshProUGUI tooltipText;
    //Detect if the Cursor starts to pass over the GameObject
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

    private void ShowMessage(string text)
    {
        tooltipText = text;
    }
}

/*public class TooltipManager : MonoBehaviour
{
    public static TooltipManager _instance;

    public TextMeshProUGUI textComponent;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void SetAndShowToolTip(string message)
    {
        gameObject.SetActive(true);
        textComponent.text = message;
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
    }
}*/
