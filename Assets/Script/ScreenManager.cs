using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScreenManager : MonoBehaviour
{
    public GameObject screenLoading;
    public GameObject screenPort;
    public GameObject screenQuai;
    public GameObject screenQuaiBig;
    public GameObject screenQuaiSmol;
    public GameObject screenPortBig;
    public GameObject screenPortSmol;
    public GameObject VictoryScreen;
    [SerializeField] private TextMeshProUGUI victoryMoneyText;
    [SerializeField] private TextMeshProUGUI defeatMoneyText;
    [SerializeField] private GameObject repairButton = null;
    [SerializeField] private GameObject livreCounter = null;
    [SerializeField] private GameObject lifeBar = null;
    public GameObject DefeatBreakScreen;
    public GameObject DefeatMoneyScreen;
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

        VictoryScreen.SetActive(false);
        DefeatMoneyScreen.SetActive(false);
        DefeatBreakScreen.SetActive(false);

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

        repairButton.SetActive(PortManager.instance.isRepairEnable);
        livreCounter.SetActive(true);
        lifeBar.SetActive(true);

        MerchandiseManager.instance.wipeMerchandise();
        MerchandiseManager.instance.ConsumeFood();
        MerchandiseManager.instance.spawnRation();
        PortManager.instance.SpawnMarchandises();
        PortManager.instance.VenteMarchandises();


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


        // Fin du jeu
        if(PortManager.instance._portEvent.CityName == ECityNames.NANTES) {
            if (RessourcesManager.instance._money > 0) {
                VictoryScreen.SetActive(true);
                victoryMoneyText.text = RessourcesManager.instance._money.ToString();
                AudioManager.instance.ChangBGM(11);
            } else {

                DefeatMoneyScreen.SetActive(true);
                AudioManager.instance.ChangBGM(12);
                defeatMoneyText.text = RessourcesManager.instance._money.ToString();
            }
        }
    }
    public void SetPortScreen(bool isBig)
    {
        PortManager.instance.LoadData();
        SetActiveScreen(1);
        repairButton.SetActive(false);
        livreCounter.SetActive(false);
        lifeBar.SetActive(false);

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
        GridManager.instance.applyInventoryDamage();
        loadingCamera.gameObject.SetActive(false);
        loadingCanvas.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(true);
        SetPortScreen(PortManager.instance._portEvent.bigPort);
    }


    public void SetDefeatBreakScreen() {
        DefeatBreakScreen.SetActive(true);
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
