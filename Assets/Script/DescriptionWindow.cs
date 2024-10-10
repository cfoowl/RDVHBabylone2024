using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DescriptionWindow : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI titleText = null;
    [SerializeField] private TextMeshProUGUI buyPrice = null;
    [SerializeField] private TextMeshProUGUI sellPrice = null;
    public static DescriptionWindow instance;
    void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void setTitleText(string itemName) {
        titleText.text = itemName;
    }

    public void setBuyPrice(string price) {
        buyPrice.text = price;
    }

    public void setSellPrice(string price) {
        sellPrice.text = price;
    }

    public void openWindow() {
        gameObject.SetActive(true);
    }

    public void closeWindow() {
        gameObject.SetActive(false);
    }

}
