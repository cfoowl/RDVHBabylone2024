using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PortManager : MonoBehaviour
{
    #region Fields


    public static PortManager instance;
    [SerializeField] public EventDatas _portEvent = null;
    [SerializeField] private TextMeshProUGUI _eventNameText = null;
    [SerializeField] private TextMeshProUGUI _eventTextText = null;
    // [SerializeField] private Image _eventImageImage = null;
    [SerializeField] private RessourcesManager _ressourcesManager = null;
    [SerializeField] private GameObject _continueButton = null;
    [SerializeField] private GameObject _nextPortButton = null;
    [SerializeField] private GameObject _specialChoiceButtons = null;
    [SerializeField] private TextMeshProUGUI _specialButton1Text = null;
    [SerializeField] private TextMeshProUGUI _specialButton2Text = null;
    [SerializeField] private GameObject ancenisButton = null;
    public bool isRepairEnable;
    [SerializeField, TextArea(10, 9999)] private string _specialTextTours = "";
    [SerializeField, TextArea(10, 9999)] private string _specialTextAngers = "";
    [SerializeField] private GameObject _victoryScreen = null;
    private GameFlowManager _gameFlowManager = null;


    #endregion Fields

    #region Properties

    public EventDatas PortEvent
    {
        get
        {
            return _portEvent;
        }

        set
        {
            _portEvent = value;
        }
    }

    public GameFlowManager GameFlowManager
    {
        get
        {
            return _gameFlowManager;
        }

        set
        {
            _gameFlowManager = value;
        }
    }

    #endregion Properties

    #region Methods

    private void Start()
    {
        instance = this;
        isRepairEnable = false;
    }

    //Load Datas from the EventDatas to the UI
    public void LoadData()
    {
        if (_portEvent != null)
        {
            changeBGMusic(_portEvent.CityName);
            changeMonologue(_portEvent.CityName);
            _eventNameText.text = _portEvent.EventName;
            StartCoroutine(DisplayText(_portEvent.EventText));
            _continueButton.SetActive(true);

            TriggerSpecialEvent(_portEvent.CityName);
        }
    }

    //Make Text appear letter by letter
    public IEnumerator DisplayText(string text)
    {
        _eventTextText.text = string.Empty;
        for (int i = 0; i < text.Length; i++)
        {
            _eventTextText.text += text[i];
            yield return new WaitForSeconds(0.005f);
        }
        yield return null;
    }

    public void SpawnMarchandises()
    {
        foreach (EMarchandiseTypes type in _portEvent.EventMarchandisesGained)
        {
            MerchandiseManager.instance.spawnMerchandise(type);
        }
    }
    public void VenteMarchandises()
    {
        bool flag = false;
        foreach (EMarchandiseTypes type in _portEvent.EventMarchandisesRemoved)
        {
            if (MerchandiseManager.instance.sellMerchandise(type))
            {
                flag = true;
            }
        }
        if (flag) {
            SoundManager.instance.audioSource.clip = SoundManager.instance.coins;
            SoundManager.instance.audioSource.Play();
        }
    }

    public void ContinueButton()
    {
        StopAllCoroutines();
        ScreenManager.instance.SetQuaiScreen(_portEvent.bigPort);
        StartCoroutine(DisplayText(_portEvent.EventTextPart2[0]));
        _continueButton.SetActive(false);
        _nextPortButton.SetActive(true);
    }

    public void GoToNextPortButton()
    {
        if (MerchandiseManager.instance.isInInventory(EMarchandiseTypes.RATION))
        {
            StopAllCoroutines();
            _gameFlowManager.MoveToNextPort();
            _nextPortButton.SetActive(false);
        }
        else
        {
            Debug.Log("Not enough food !");
        }
    }


    #region SpecialEvent

    //Check if there is a special event based on the port name
    public void TriggerSpecialEvent(ECityNames portName)
    {
        switch (portName)
        {
            case ECityNames.AMBOISE:
                EventAmboise();
                break;
            case ECityNames.SAUMUR:
                _continueButton.SetActive(false);
                _specialChoiceButtons.SetActive(true);
                _specialButton1Text.text = _portEvent.Button1Text;
                _specialButton2Text.text = _portEvent.Button2Text;

                break;

            case ECityNames.ANGERS:
                EventAngers();
                break;
            case ECityNames.CHALONNES:
                EventChalonnes();
                break;
            case ECityNames.ANCENIS:
                if (MerchandiseManager.instance.isInInventory(EMarchandiseTypes.SEL)) {
                    _continueButton.SetActive(false);
                    ancenisButton.SetActive(true);
                }
                break;

            case ECityNames.NANTES:
                EventNantes();
                break;

            default:
                break;
        }
    }
    public void changeBGMusic(ECityNames portName)
    {
        switch (portName)
        {
            case ECityNames.ORLEANS:
                AudioManager.instance.ChangBGM(7);
                break;
            case ECityNames.BEAUGENCY:
                AudioManager.instance.ChangBGM(3);
                break;
            case ECityNames.BLOIS:
                AudioManager.instance.ChangBGM(4);
                break;
            case ECityNames.AMBOISE:
                AudioManager.instance.ChangBGM(1);
                break;
            case ECityNames.TOURS:
                AudioManager.instance.ChangBGM(10);
                break;
            case ECityNames.SAUMUR:
                AudioManager.instance.ChangBGM(9);
                break;
            case ECityNames.ANGERS:
                AudioManager.instance.ChangBGM(8);
                break;
            case ECityNames.CHALONNES:
                AudioManager.instance.ChangBGM(5);
                break;
            case ECityNames.ANCENIS:
                AudioManager.instance.ChangBGM(2);
                break;
            case ECityNames.NANTES:
                AudioManager.instance.ChangBGM(6);
                break;
        }
    }
    public void changeMonologue(ECityNames portName)
    {
        switch (portName)
        {
            case ECityNames.ORLEANS:
                MonologueManager.instance.playMonologue(0);
                break;
            case ECityNames.BEAUGENCY:
                MonologueManager.instance.playMonologue(1);
                break;
            case ECityNames.BLOIS:
                MonologueManager.instance.playMonologue(2);
                break;
            case ECityNames.AMBOISE:
                MonologueManager.instance.playMonologue(3);
                break;
            case ECityNames.TOURS:
                MonologueManager.instance.playMonologue(4);
                break;
            case ECityNames.SAUMUR:
                MonologueManager.instance.playMonologue(5);
                break;
            case ECityNames.ANGERS:
                MonologueManager.instance.playMonologue(6);
                break;
            case ECityNames.CHALONNES:
                MonologueManager.instance.playMonologue(7);
                break;
            case ECityNames.ANCENIS:
                if(MerchandiseManager.instance.isInInventory(EMarchandiseTypes.SEL)) {
                    MonologueManager.instance.playMonologue(9);
                } else {
                    MonologueManager.instance.playMonologue(8);
                }
                break;
            case ECityNames.NANTES:
                MonologueManager.instance.playMonologue(13);
                break;
        }
    }

    public void EventOrleans() {
        Popup.instance.changeTitle("Tutoriel");
        Popup.instance.catToText(Popup.instance.TutorialText);
        Popup.instance.openPopup();
    }
    private void EventAmboise() {
        isRepairEnable = true;
    }
    private void EventAngers() {
        RessourcesManager.instance.UseMoney(40);
    }
    private void EventChalonnes() {
        int currentHealth = RessourcesManager.instance._health;
        if(currentHealth >=2) {
            double tmp1 = Math.Pow(10,currentHealth-2)/ Factorial(currentHealth-2);
            double tmp2 = Math.Exp(2-currentHealth);
            double damage = Math.Ceiling(tmp1 * tmp2 * 0.3);
            RessourcesManager.instance.applyDamage((float)damage);

        }
    }
    private double Factorial(double n) {
        if (n == 0 || n == 1)
            return 1;
        else
            return n * Factorial(n - 1);
    }
    public void ancenisButtonAction() {
        ancenisButton.SetActive(false);
        _specialChoiceButtons.SetActive(true);
        _specialButton1Text.text = _portEvent.Button1Text;
        _specialButton2Text.text = _portEvent.Button2Text;
        StopAllCoroutines();
        StartCoroutine(DisplayText(_portEvent.EventTextPart2[1]));
        MonologueManager.instance.playMonologue(10);
    }
    private void EventNantes()
    {
        _continueButton.SetActive(true);
    }

    private void SpecialButton(int choice) {
        int nextTextIndex = 0;
        switch (_portEvent.CityName) {
            case ECityNames.SAUMUR:
                if (choice == 1) {
                    nextTextIndex = 0;
                } else {
                    nextTextIndex = 1;
                    MerchandiseManager.instance.spawnMerchandise(EMarchandiseTypes.SEL);
                }
                _nextPortButton.SetActive(true);
                ScreenManager.instance.SetQuaiScreen(_portEvent.bigPort);
                break;

            case ECityNames.ANCENIS:
                if (choice == 1) {
                    nextTextIndex = 2;
                    MerchandiseManager.instance.deleteMerchandise(EMarchandiseTypes.SEL);
                    RessourcesManager.instance.UseMoney(300);
                    MonologueManager.instance.playMonologue(11);
                } else {
                    nextTextIndex = 3;
                    MonologueManager.instance.playMonologue(12);
                }
                _continueButton.SetActive(true);
                break;
        }

        _specialChoiceButtons.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(DisplayText(_portEvent.EventTextPart2[nextTextIndex]));
    }
    public void SpecialButton1()
    {
        SpecialButton(1);
    }

    public void SpecialButton2()
    {
        SpecialButton(2);
    }

    public void RepairButton() {
        if (RessourcesManager.instance._health < 10) {
            if (_portEvent.bigPort) {
                RessourcesManager.instance.UseMoney(150);
                RessourcesManager.instance.repairBoat(10);
            } else {
                RessourcesManager.instance.UseMoney(75);
                RessourcesManager.instance.repairBoat(2);
            }

        }
    }


    #endregion SpecialEvent

    #endregion Methods
}
