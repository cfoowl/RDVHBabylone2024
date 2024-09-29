using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "EventData", menuName = "ScriptableObjects/EventData")]
public class EventDatas : ScriptableObject
{
    #region Fields

    [SerializeField] private ECityNames _cityName = ECityNames.NONE;
    [SerializeField] private string _eventName = "";
    [SerializeField,TextArea(10, 9999)] private string _eventText = "";
    [SerializeField, TextArea(10, 9999)] private string _eventTextPart2 = "";
    [SerializeField] private string _button1Text = "";
    [SerializeField] private string _button2Text = "";
    [SerializeField] private Sprite _eventImage = null;
    [SerializeField] private EMarchandiseTypes[] _eventMarchandisesGained = null;
    [SerializeField] private EMarchandiseTypes[] _eventMarchandisesRemoved = null;
    [SerializeField] private AudioSource _eventAudioSource = null;

    #endregion Fields

    #region Properties

    public ECityNames CityName => _cityName;
    public string EventName => _eventName;
    public string EventText => _eventText;
    public string EventTextPart2 => _eventTextPart2;
    public string Button1Text => _button1Text;
    public string Button2Text => _button2Text;
    public Sprite EventImage => _eventImage;
    public EMarchandiseTypes[] EventMarchandisesGained => _eventMarchandisesGained;
    public EMarchandiseTypes[] EventMarchandisesRemoved => _eventMarchandisesRemoved;
    public AudioSource EventAudioSource => _eventAudioSource;

    #endregion Properties
}
