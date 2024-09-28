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
            Debug.Log("Data Loaded from " + _portEvent.name);
        }
    }

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


    #region SpecialEvent

    //Check if there is a special event based on the port name
    public void TriggerSpecialEvent(string portName)
    {
        switch (portName)
        {
            case "Blois":
                EventBlois();
                Debug.Log("Special Event Blois Triggered");
                break;

            case "Tours":
                EventTours();
                Debug.Log("Special Event Tours Triggered");
                break;

            case "Angers":
                EventAngers();
                Debug.Log("Special Event Angers Triggered");
                break;

            case "Nantes":
                EventNantes();
                Debug.Log("Special Event Nantes Triggered");
                break;

            default:
                Debug.Log("No Special Event in this Port");
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
