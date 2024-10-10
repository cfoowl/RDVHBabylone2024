using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RessourcesManager : MonoBehaviour
{
    #region Fields

    [SerializeField] public float _money = 200;
    [SerializeField] public int _health = 10;
    [SerializeField] public Image LifeBar;
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

    public void applyDamage(float damage) {
        _health -= (int)Math.Floor(damage);
        if (_health <= 0) {
            ScreenManager.instance.SetDefeatBreakScreen();
            AudioManager.instance.ChangBGM(12);
        }
        if (_health <= 3) {
            LifeBar.color = Color.red;
        } else {
            LifeBar.color = Color.white;
        }
    }

    public void repairBoat(int health) {
        _health += health;
        SoundManager.instance.audioSource.clip = SoundManager.instance.repair;
        SoundManager.instance.audioSource.Play();
        if (_health > 10) {
            _health = 10;
        }
        if (_health <= 3) {
            LifeBar.color = Color.red;
        } else {
            LifeBar.color = Color.white;
        }
    }

    //Event called when ther is not enough Money to pay Something
    public void OnNotEnoughMoneyEv()
    {
        // _onNotEnoughMoney();
        return;
    }

    // public int computeDebt(int initialValue) {
    //     if (_money < 0) {
    //         return Math.Floor(initialValue * (1f+(-_money/100f)));
    //     } else {
    //         return initialValue;
    //     }
    // }
    #endregion Money

    #endregion Methods
    void Start() {
        instance = this;
    }
}
