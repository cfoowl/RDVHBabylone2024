using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{

    #region Fields
    public static GameFlowManager instance;

    [SerializeField] private PortManager _port = null;
    [SerializeField] private RessourcesManager _ressources = null;
    [SerializeField] private EventDatas[] _events = new EventDatas[0];
    [SerializeField] private EventDatas[] _contrebandEvents = new EventDatas[0];
    [SerializeField] private GameObject _transitionScreen = null;
    [SerializeField] private Animator _transitionAnim = null;
    //[SerializeField] private GameObject _rationPopUp = null;
    //[SerializeField] private GameObject _lostMarchandisePopUp = null;
    private bool _contreband = false;
    private int _currentEvent = 0;
    private bool _bloisSkip = false;

    #endregion Fields

    void Awake()
    {
        instance = this;
    }
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

    public bool BloisSkip
    {
        get
        {
            return _bloisSkip;
        }

        set
        {
            _bloisSkip = value;
        }
    }

    public int CurrentEvent
    {
        get
        {
            return _currentEvent;
        }
        set
        {
            _currentEvent = value;
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

    }
    private void ReinitializeGame()
    {
        _currentEvent = 0;
        _ressources.ReinitializeMoney();
        _port.PortEvent = _events[_currentEvent];
        ScreenManager.instance.EnterLoadingScreen();
    }

    public void MoveToNextPort()
    {
        GridManager.instance.applyInventoryDamage();
        if (_currentEvent + 1 < _events.Length)
        {
            _currentEvent++;
            if (MerchandiseManager.instance.isInInventory(EMarchandiseTypes.SEL))
            {
                _port.PortEvent = _contrebandEvents[_currentEvent];
            }
            else
            {
                _port.PortEvent = _events[_currentEvent];
            }
            ScreenManager.instance.EnterLoadingScreen();
        }
    }

    private IEnumerator Transition()
    {
        // _transitionAnim.Play("TransitionOn");
        yield return new WaitForSeconds(5f);
        _port.LoadData();
        yield return new WaitForSeconds(0.2f);
        // _transitionAnim.Play("TransitionOff");
        yield return new WaitForSeconds(0.5f);
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
    public void ReloadGameButton() {
        SceneManager.LoadScene("FullSceneTest");
    }

    public void MenuButton() {
        SceneManager.LoadScene("UI_Menu_start");
    }
    #endregion Methods



}
