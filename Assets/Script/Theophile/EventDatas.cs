using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "EventData", menuName = "ScriptableObjects/EventData")]
public class EventDatas : ScriptableObject
{
    #region Fields

    [SerializeField] private string _portName = "";
    [SerializeField] private string _eventName = "";
    [SerializeField,TextArea] private string _eventText = "";
    [SerializeField] private Sprite _eventImage = null;
    [SerializeField] private EMarchandiseTypes[] _eventMarchandisesGained = null;
    [SerializeField] private EMarchandiseTypes[] _eventMarchandisesRemoved = null;


    #endregion Fields

    #region Properties

    public string PortName => _portName;
    public string EventName => _eventName;
    public string EventText => _eventText;
    public Sprite EventImage => _eventImage;
    public EMarchandiseTypes[] EventMarchandisesGained => _eventMarchandisesGained;
    public EMarchandiseTypes[] EventMarchandisesRemoved => _eventMarchandisesRemoved;

    #endregion Properties
}
