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
    public AudioClip victory;
    public AudioClip defeat;


    void Start() {
        instance = this;
    }

}
