using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    AudioSource TitleLoop;
    AudioSource AmboiseLoop;
    AudioSource AncenisLoop;
    AudioSource BeaugencyLoop;
    AudioSource BloisLoop;
    AudioSource ChalonnesLoop;
    AudioSource NantesLoop;
    AudioSource OrleansLoop;
    AudioSource PontDeLoop;
    AudioSource SaumurLoop;
    AudioSource ToursLoop;
    AudioSource VictorySound;
    AudioSource DefeatSound;
    public bool mute;
    public float masterVolume;
    public float fadeDuration = 2f;
    private int currentMusicIndex = 0;
    List<AudioSource> musicSources = new List<AudioSource>();

    // Start is called before the first frame update
    void Start()
    {
        musicSources.Add(TitleLoop);
        musicSources.Add(AmboiseLoop);
        musicSources.Add(AncenisLoop);
        musicSources.Add(BeaugencyLoop);
        musicSources.Add(BloisLoop);
        musicSources.Add(ChalonnesLoop);
        musicSources.Add(NantesLoop);
        musicSources.Add(OrleansLoop);
        musicSources.Add(PontDeLoop);
        musicSources.Add(SaumurLoop);
        musicSources.Add(ToursLoop);
        musicSources.Add(VictorySound);
        musicSources.Add(DefeatSound);
        instance = this;
        for(int i = 0; i < musicSources.Count; i++) {
            musicSources[i] = transform.GetChild(i).GetComponent<AudioSource>();
            musicSources[i].Play();
            musicSources[i].loop = true;
            musicSources[i].volume = 0;
        }

    }

    public void SetBGM(int index) {
        stopMusic();
        musicSources[index].volume = masterVolume;
    }
    public void ChangBGM(int index) {
        StartCoroutine(FadeMusic(currentMusicIndex, index));
    }
    public void stopMusic() {
        for(int i = 0; i < musicSources.Count; i++) {
            musicSources[i].volume = 0;
        }
    }
    public void changeVolume() {
        musicSources[currentMusicIndex].volume = masterVolume;
    }

    private IEnumerator FadeMusic(int oldSource, int newSource)
    {
        musicSources[newSource].volume = 0f;

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / fadeDuration;

            musicSources[oldSource].volume = Mathf.Lerp(masterVolume, 0f, progress);
            musicSources[newSource].volume = Mathf.Lerp(0f, masterVolume, progress);

            yield return null;
        }
        musicSources[oldSource].volume = 0;
        currentMusicIndex = newSource;
    }


}
