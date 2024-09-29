using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    AudioSource TitleScreenLoop;
    AudioSource AmboiseLoop;
    AudioSource AncenisLoop;
    AudioSource BeaugencyLoop;
    AudioSource NantesLoop;
    AudioSource OrleansBloisChalonnesLoop;
    AudioSource PontDeLoop;
    AudioSource SaumurLoop;
    AudioSource ToursLoop;
    public bool mute;
    public float masterVolume;
    List<AudioSource> musicSources = new List<AudioSource>();

    // Start is called before the first frame update
    void Start()
    {
        musicSources.Add(TitleScreenLoop);
        musicSources.Add(AmboiseLoop);
        musicSources.Add(AncenisLoop);
        musicSources.Add(BeaugencyLoop);
        musicSources.Add(NantesLoop);
        musicSources.Add(OrleansBloisChalonnesLoop);
        musicSources.Add(PontDeLoop);
        musicSources.Add(SaumurLoop);
        musicSources.Add(ToursLoop);
        instance = this;
        for(int i = 0; i < musicSources.Count; i++) {
            musicSources[i] = transform.GetChild(i).GetComponent<AudioSource>();
            musicSources[i].Play();
            musicSources[i].loop = true;
            musicSources[i].volume = 0;
        }

    }

    public void ChangBGM(int index) {
        stopMusic();
        musicSources[index].volume = masterVolume;
    }
    public void stopMusic() {
        for(int i = 0; i < musicSources.Count; i++) {
            musicSources[i].volume = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
