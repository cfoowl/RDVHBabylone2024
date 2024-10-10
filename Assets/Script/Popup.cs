using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{

    public static Popup instance;    
    private string title = "";
    private string core = "";
    [SerializeField,TextArea(10, 9999)] public string TutorialText = "";


    [SerializeField] private TextMeshProUGUI titleText = null;
    [SerializeField] private TextMeshProUGUI coreText = null;


    void Awake() {
        instance = this;
        gameObject.SetActive(false);
    }
    public void openPopup() {
        gameObject.SetActive(true);
        titleText.text = title;
        coreText.text = core;

    }
    public void closePopup() {
        gameObject.SetActive(false);
    }
    public void changeTitle(string text) {
        title = text;
    }

    public void clearText() {
        core = "";
    }
    public void catToText(string text) {
        core += text;
    }
}
