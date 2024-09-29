using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PortManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private EventDatas _portEvent = null;
    [SerializeField] private TextMeshProUGUI _eventNameText = null;
    [SerializeField] private TextMeshProUGUI _eventTextText = null;
    [SerializeField] private Image _eventImageImage = null;
    [SerializeField] private RessourcesManager _ressourcesManager = null;
    [SerializeField] private GameObject _continueButton = null;
    [SerializeField] private GameObject _nextPortButton = null;
    [SerializeField] private GameObject _specialChoiceButtons = null;
    [SerializeField] private TextMeshProUGUI _specialButton1Text = null;
    [SerializeField] private TextMeshProUGUI _specialButton2Text = null;
    [SerializeField, TextArea(10, 9999)] private string _specialTextBlois = "";
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
        
    }

    //Load Datas from the EventDatas to the UI
    public void LoadData()
    {
        if (_portEvent != null)
        {
            _eventImageImage.sprite = _portEvent.EventImage;
            _eventNameText.text = _portEvent.EventName;
            StartCoroutine(DisplayText(_portEvent.EventText));
            if(_portEvent.CityName == ECityNames.BLOIS || _portEvent.CityName == ECityNames.ANGERS)
            {
                _specialChoiceButtons.SetActive(true);
                _specialButton1Text.text = _portEvent.Button1Text;
                _specialButton2Text.text = _portEvent.Button2Text;
            }
            else if(_portEvent.CityName == ECityNames.TOURS)
            {
                TriggerSpecialEvent(ECityNames.TOURS);
                _continueButton.SetActive(true);
            }
            else if (_portEvent.CityName == ECityNames.NANTES)
            {
                TriggerSpecialEvent(ECityNames.NANTES);
            }
            else
            {
                if (_portEvent.EventTextPart2 != "")
                {
                    _continueButton.SetActive(true);
                }
                else
                {
                    _nextPortButton.SetActive(true);
                }
            }

            SpawnMarchandises();
            VenteMarchandises();
            changeBGMusic(_portEvent.CityName);

            Debug.Log("Data Loaded from " + _portEvent.name);
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

    private void SpawnMarchandises()
    {
        foreach(EMarchandiseTypes type in _portEvent.EventMarchandisesGained) {
            MerchandiseManager.instance.spawnMerchandise(type);
        } 
    }
    private void VenteMarchandises() {
        foreach(EMarchandiseTypes type in _portEvent.EventMarchandisesRemoved) {
            MerchandiseManager.instance.sellMerchandise(type);
        }
    }

    public void ContinueButton()
    {
        if (_portEvent.EventTextPart2 != "")
        {
            StopAllCoroutines();
            if (_portEvent.CityName == ECityNames.TOURS && _gameFlowManager.BloisSkip == true)
            {
                StartCoroutine(DisplayText(_specialTextTours));
                _gameFlowManager.BloisSkip = false;
            }

            else
            {
                StartCoroutine(DisplayText(_portEvent.EventTextPart2));
                _continueButton.SetActive(false);
                _nextPortButton.SetActive(true);
            }
        }
        else if (_portEvent.CityName == ECityNames.NANTES)
        {
            _victoryScreen.SetActive(true);
            AudioManager.instance.stopMusic();
            SoundManager.instance.audioSource.clip = SoundManager.instance.victory;
            SoundManager.instance.audioSource.Play();
        }
    }

    public void GoToNextPortButton()
    {
        StopAllCoroutines();
        MerchandiseManager.instance.wipeMerchandise();
        _gameFlowManager.MoveToNextPort();
        _nextPortButton.SetActive(false);
    }


    #region SpecialEvent

    //Check if there is a special event based on the port name
    public void TriggerSpecialEvent(ECityNames portName)
    {
        switch (portName)
        {
            case ECityNames.BLOIS:
                EventBlois();
                Debug.Log("Special Event Blois Triggered");
                break;

            case ECityNames.TOURS:
                EventTours();
                Debug.Log("Special Event Tours Triggered");
                break;

            case ECityNames.ANGERS:
                EventAngers();
                Debug.Log("Special Event Angers Triggered");
                break;

            case ECityNames.NANTES:
                EventNantes();
                Debug.Log("Special Event Nantes Triggered");
                break;


            default:
                Debug.Log("No Special Event in this Port");
                SpawnMarchandises();
                break;
        }
    }
    public void changeBGMusic(ECityNames portName) {
        switch (portName) {
            case ECityNames.ORLEANS:
                AudioManager.instance.ChangBGM(5);
                break;
            case ECityNames.BEAUGENCY:
                AudioManager.instance.ChangBGM(3);
                break;
            case ECityNames.BLOIS:
                AudioManager.instance.ChangBGM(5);
                break;
            case ECityNames.AMBOISE:
                AudioManager.instance.ChangBGM(1);
                break;
            case ECityNames.TOURS:
                AudioManager.instance.ChangBGM(8);
                break;
            case ECityNames.SAUMUR:
                AudioManager.instance.ChangBGM(7);
                break;
            case ECityNames.ANGERS:
                AudioManager.instance.ChangBGM(6);
                break;
            case ECityNames.CHALONNES:
                AudioManager.instance.ChangBGM(5);
                break;
            case ECityNames.ANCENIS:
                AudioManager.instance.ChangBGM(2);
                break;
            case ECityNames.NANTES:
                AudioManager.instance.ChangBGM(4);
                break;
        }
    }

    //Event that triggers when arriving at Blois' Port
    private void EventBlois()
    {
        _gameFlowManager.CurrentEvent++;
        _gameFlowManager.BloisSkip = true;
        MerchandiseManager.instance.wipeMerchandise();
    }

    //Event that triggers when arriving at Tours' Port
    private void EventTours()
    {
        _ressourcesManager.UseMoney(220);
        if(_gameFlowManager.BloisSkip == true)
        {
            _ressourcesManager.AddMoney(50);
        }
    }

    //Event that triggers when arriving at Angers' Port
    private void EventAngers()
    {
        _gameFlowManager.Contreband = true;
        MerchandiseManager.instance.wipeMerchandise();
        MerchandiseManager.instance.spawnMerchandise(EMarchandiseTypes.SEL);
    }

    private void EventNantes()
    {
        _continueButton.SetActive(true);
    }

    public void SpecialButton1()
    {
        if (_portEvent.CityName == ECityNames.BLOIS)
        {
            StopAllCoroutines();
            StartCoroutine(DisplayText(_portEvent.EventTextPart2));
        }
        else
        {
            _ressourcesManager.UseMoney(40);
            StopAllCoroutines();
            StartCoroutine(DisplayText(_portEvent.EventTextPart2));
        }
        _specialChoiceButtons.SetActive(false);
        _nextPortButton.SetActive(true);
    }

    public void SpecialButton2()
    {
        if (_portEvent.CityName == ECityNames.BLOIS)
        {
            TriggerSpecialEvent(ECityNames.BLOIS);
            StopAllCoroutines();
            StartCoroutine(DisplayText(_specialTextBlois));
        }
        else
        {
            _ressourcesManager.AddMoney(80);
            TriggerSpecialEvent(ECityNames.ANGERS);
            StopAllCoroutines();
            StartCoroutine(DisplayText(_specialTextAngers));
        }
        _specialChoiceButtons.SetActive(false);
        _nextPortButton.SetActive(true);
    }

    #endregion SpecialEvent

    #endregion Methods
}
