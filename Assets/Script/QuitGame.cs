using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitBouton : MonoBehaviour
{
    public void OuvrirLien(string lien)
    {
        // Ouvre le lien dans le navigateur
        Application.OpenURL(lien);
    }

    public void QuitterJeu()
    {
        // Quitte l'application 
        Application.Quit();
    }
}