using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public GameObject screenLoading;
    public GameObject screenPort;
    public GameObject screenQuai;
    public GameObject screenQuaiBig;
    public GameObject screenQuaiSmol;
    public Camera mainCamera;
    public Canvas mainCanvas;
    public Camera loadingCamera;
    public Canvas loadingCanvas;
    public GameObject loadingBoat;

    private int currentScreen = 1;
    public static ScreenManager instance;
    private GameObject[] screens;
    void Awake()
    {
        instance = this;
        screens = new GameObject[] { screenLoading, screenPort, screenQuai };

    }
    void Start()
    {
    }

    void Initialize()
    {
        currentScreen = 1;
        SetActiveScreen(currentScreen);
    }

    public void SetQuaiScreen(bool isBig)
    {
        SetActiveScreen(2);
        if (isBig)
        {
            screenQuaiBig.SetActive(true);
        }
        else
        {
            screenQuaiSmol.SetActive(true);
        }
    }
    public void SetPortScreen()
    {
        PortManager.instance.LoadData();
        SetActiveScreen(1);
    }
    public void EnterLoadingScreen()
    {
        loadingCamera.gameObject.SetActive(true);
        loadingCanvas.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(false);
        SetActiveScreen(0);
        loadingCamera.GetComponent<CameraFollow>().ContinueMoving();
        loadingBoat.GetComponent<LoadingBoat>().ContinueMoving();
    }
    public void ExitLoadingScreen()
    {
        loadingCamera.gameObject.SetActive(false);
        loadingCanvas.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(true);
        SetPortScreen();
    }

    private void SetActiveScreen(int index)
    {
        Debug.Log(screens);
        screens[currentScreen].SetActive(false);
        if (currentScreen == 2)
        {
            screenQuaiSmol.SetActive(false);
            screenQuaiBig.SetActive(false);
        }
        screens[index].SetActive(true);
        currentScreen = index;
    }
}
