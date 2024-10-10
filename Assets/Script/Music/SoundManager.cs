using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource audioSource;
    public AudioClip choice;
    public AudioClip[] clic; 
    public AudioClip[] DragDrop;
    public AudioClip coins;


    void Start() {
        instance = this;
    }

}
