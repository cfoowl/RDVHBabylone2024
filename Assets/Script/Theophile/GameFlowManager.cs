using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{

    #region Fields

    [SerializeField] private PortManager _port = null;
    [SerializeField] private RessourcesManager _ressources = null;
    [SerializeField] private EventDatas[] _events = new EventDatas[0];
    [SerializeField] private EventDatas[] _contrebandEvents = new EventDatas[0];
    [SerializeField] private GameObject _transitionScreen = null;
    [SerializeField] private Animator _transitionAnim = null;
    [SerializeField] private GameObject _rationPopUp = null;
    [SerializeField] private GameObject _lostMarchandisePopUp = null;
    private bool _contreband = false;
    private int _currentEvent = 0;

    #endregion Fields

    public bool Contreband
    {
        get
        {
            return _contreband;
        }

        set
        {
            _contreband = value;
        }
    }

    #region Methods


    private void Start()
    {
        ReinitializeGame();
        _port.GameFlowManager = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _port.StopAllCoroutines();
            MoveToNextPort();
        }
    }
    private void ReinitializeGame()
    {
        _currentEvent = 0;
        _ressources.ReinitializeMoney();
        _port.PortEvent = _events[_currentEvent];
        StartCoroutine(Transition());
    }

    public void MoveToNextPort()
    {
        if (_currentEvent+1 < _events.Length)
        {
            _currentEvent++;
            if (Contreband)
            {
                _port.PortEvent = _contrebandEvents[_currentEvent];
            }
            else
            {
                _port.PortEvent = _events[_currentEvent];
            }
            StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition()
    {
        _transitionScreen.SetActive(true);
        _transitionAnim.Play("TransitionOn");
        yield return new WaitForSeconds(2.8f);
        _port.LoadData();
        yield return new WaitForSeconds(0.2f);
        _transitionAnim.Play("TransitionOff");
        yield return new WaitForSeconds(0.5f);
        _transitionScreen.SetActive(false);
    }
    #endregion Methods

    public void GoToNextPortButton()
    {
        /*
        if (rationcale > 0)
        {
            rationcale--;
            MoveToNextPort();
        }
        else
        {
            spawn _rationpopup
        }
        */
    }


}
