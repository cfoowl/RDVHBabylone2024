using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonColorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI buttonText;
    // Color hovercolor = new Color(90,117,151,255);
    Color originalColor = new Color(0.8196079f,0.8588235f,0.9058824f,1f);
    Color hoverColor = new Color(0.776f, 0.655f, 0.463f, 1f);

    void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        print(hoverColor);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        buttonText.color = hoverColor;
    }
    public void OnPointerExit(PointerEventData eventData) {
        buttonText.color = originalColor;
    }
}
