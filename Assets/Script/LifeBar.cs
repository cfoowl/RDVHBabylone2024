using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    Image image;
    void Start() {
        image = GetComponent<Image>();
    }
    void Update()
    {
        image.fillAmount = RessourcesManager.instance._health / 10f;
    }
}
