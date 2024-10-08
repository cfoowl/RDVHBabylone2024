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
    public GameObject screenPortBig;
    public GameObject screenPortSmol;
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
        screenLoading.SetActive(false);
        screenPort.SetActive(false);
        screenQuai.SetActive(false);
        screenQuaiBig.SetActive(false);
        screenQuaiSmol.SetActive(false);
        mainCamera.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(false);

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
        Debug.Log(isBig);
        SetActiveScreen(2);

        MerchandiseManager.instance.wipeMerchandise();
        MerchandiseManager.instance.ConsumeFood();
        MerchandiseManager.instance.spawnRation();


        if (isBig)
        {
            screenQuaiSmol.SetActive(false);
            screenQuaiBig.SetActive(true);
        }
        else
        {
            screenQuaiBig.SetActive(false);
            screenQuaiSmol.SetActive(true);
        }
    }
    public void SetPortScreen(bool isBig)
    {
        PortManager.instance.LoadData();
        SetActiveScreen(1);

        if (isBig)
        {
            screenPortSmol.SetActive(false);
            screenPortBig.SetActive(true);
        }
        else
        {
            screenPortBig.SetActive(false);
            screenPortSmol.SetActive(true);
        }
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
        SetPortScreen(PortManager.instance._portEvent.bigPort);
    }

    private void SetActiveScreen(int index)
    {
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
