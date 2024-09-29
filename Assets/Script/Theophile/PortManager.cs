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
    [SerializeField] private GameObject _specialChoiceButtons = null;
    [SerializeField] private TextMeshProUGUI _specialButton1Text = null;
    [SerializeField] private TextMeshProUGUI _specialButton2Text = null;
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
            else
            {
                _continueButton.SetActive(true);
            }

            SpawnMarchandises();


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
        switch(_portEvent.CityName){
            case ECityNames.ORLEANS:
                MerchandiseManager.instance.spawnMerchandise(EMarchandiseTypes.CEREALE);
                MerchandiseManager.instance.spawnMerchandise(EMarchandiseTypes.CEREALE);
                MerchandiseManager.instance.spawnMerchandise(EMarchandiseTypes.CEREALE);
                MerchandiseManager.instance.spawnMerchandise(EMarchandiseTypes.RATION);
                break;
        }
    }

    public void ContinueButton()
    {
        if (_portEvent.EventTextPart2 != "")
        {
            StopAllCoroutines();
            StartCoroutine(DisplayText(_portEvent.EventTextPart2));
        }
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

    //Event that triggers when arriving at Blois' Port
    private void EventBlois()
    {

    }

    //Event that triggers when arriving at Tours' Port
    private void EventTours()
    {
        _ressourcesManager.UseMoney(200);
        SpawnMarchandises();
    }

    //Event that triggers when arriving at Angers' Port
    private void EventAngers()
    {

    }

    private void EventNantes()
    {

    }

    #endregion SpecialEvent

    #endregion Methods
}
