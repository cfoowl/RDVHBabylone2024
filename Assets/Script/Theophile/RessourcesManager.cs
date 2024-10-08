using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RessourcesManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private float _money = 200;
    [SerializeField] private TextMeshProUGUI _moneyText = null;
    public static RessourcesManager instance;

    #endregion Fields


    #region Properties

    public float Money => _money;

    #endregion Properties

    #region Events

    private event Action _onNotEnoughMoney;
    public event Action OnNotEnoughMoney
    {
        add
        {
            _onNotEnoughMoney -= value;
            _onNotEnoughMoney += value;
        }

        remove
        {
            _onNotEnoughMoney -= value;
        }
    }

    #endregion Events

    #region Methods

    private void Update()
    {
        _moneyText.text = _money.ToString();
    }


    #region Money

    public void ReinitializeMoney()
    {
        _money = 200;
    }
    //Add Money if added money's value is over 0
    public float AddMoney(float moneyAdded)
    {
        if (moneyAdded > 0)
        {
            _money = _money + moneyAdded;
            // Debug.Log("Added " + moneyAdded + " to Money.");
            // Debug.Log("Current Money =  " + _money);
        }
        else
        {
            // Debug.Log("Can't add " + moneyAdded + " to Money because its value is negative or null.");
        }
        return _money;
    }

    //Remove Money if there is enough money and if used money's value is over 0 
    public float UseMoney(float moneyUsed)
    {
        _money = _money - moneyUsed;
        // if (moneyUsed > 0)
        // {
        //     if(_money - moneyUsed >= 0)
        //     {
        //         _money = _money - moneyUsed;
        //         // Debug.Log("Removed " + moneyUsed + " from Money.");
        //         // Debug.Log("Current Money =  " + _money);
        //     }
        //     else
        //     {
        //         OnNotEnoughMoneyEv();
        //         // Debug.Log("Can't use " + moneyUsed + " because there is not enough Money.");
        //     }
        // }
        // else
        // {
        //     // Debug.Log("Can't remove " + moneyUsed + " to Money because its value is positive or null.");
        // }
        return _money;
    }

    //Event called when ther is not enough Money to pay Something
    public void OnNotEnoughMoneyEv()
    {
        // _onNotEnoughMoney();
        return;
    }
    #endregion Money

    #endregion Methods
    void Start() {
        instance = this;
    }
}
