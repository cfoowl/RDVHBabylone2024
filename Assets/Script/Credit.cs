using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Credit : MonoBehaviour
{
    public GameObject PageCredit;
    // Start is called before the first frame update
    void Start()
    {
        PageCredit.SetActive(false);
    }

    public void creditButton() {
        PageCredit.SetActive(true);
    }

    public void returnButton() {
        PageCredit.SetActive(false);
    }


}
