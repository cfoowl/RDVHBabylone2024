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
    [SerializeField,TextArea(10, 9999)] public string FoodText = "";

    [SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject yesButton;
    [SerializeField] private GameObject noButton;



    [SerializeField] private TextMeshProUGUI titleText = null;
    [SerializeField] private TextMeshProUGUI coreText = null;


    void Awake() {
        instance = this;
        gameObject.SetActive(false);
    }
    public void openPopup(int mode) {
        gameObject.SetActive(true);
        titleText.text = title;
        coreText.text = core;
        if (mode == 0) {
            closeButton.SetActive(true);
            yesButton.SetActive(false);
            noButton.SetActive(false);
        } else if(mode == 1) {
            closeButton.SetActive(false);
            yesButton.SetActive(true);
            noButton.SetActive(true);
        }

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
    public void setRepairText() {
        changeTitle("Réparer le bateau");
        clearText();
        if (PortManager.instance._portEvent.bigPort) {
            catToText("Souhaitez-vous réparer l'intégralité de votre chaland pour 150 livres ?");
        } else {
            catToText("Souhaitez-vous entretenir votre chaland pour 75 livres ?");
        }
    }
    public void repairButton() {
        if (PortManager.instance._portEvent.bigPort) {
            RessourcesManager.instance.UseMoney(150);
            RessourcesManager.instance.repairBoat(10);
        } else {
            RessourcesManager.instance.UseMoney(75);
            RessourcesManager.instance.repairBoat(2);
        }
        closePopup();
    }
}
