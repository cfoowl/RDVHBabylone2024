using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    public string _scenetoload = "";
    public void PlayGame()
    {
        SceneManager.LoadScene(_scenetoload);
    }
}
