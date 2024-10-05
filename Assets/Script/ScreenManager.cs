using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public GameObject screenLoading;
    public GameObject screenPort;
    public GameObject screenQuai;

    private int currentScreen;
    public static ScreenManager instance;
    private GameObject[] screens;
    void Start()
    {
        instance = this;
        screens = new GameObject[] { screenLoading, screenPort, screenQuai };
    }

    void Initialize() {
        currentScreen = 1;
        SetActiveScreen(currentScreen);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextScreen();
        }
    }

    public void NextScreen()
    {
        SetActiveScreen((currentScreen + 1) % screens.Length);
    }
    
    private void SetActiveScreen(int index)
    {
        screens[currentScreen].SetActive(false);
        screens[index].SetActive(true);
        currentScreen = index;
    }
}
