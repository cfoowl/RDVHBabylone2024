using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologueManager : MonoBehaviour
{
    public static MonologueManager instance;
    AudioSource Orleans;
    AudioSource Beaugency;
    AudioSource Blois;
    AudioSource Amboise;
    AudioSource Tours;
    AudioSource Saumur;
    AudioSource Angers;
    AudioSource Chalonnes;
    AudioSource AncenisHonnete;
    AudioSource AncenisContr1;
    AudioSource AncenisContr2;
    AudioSource AncenisContr3_1;
    AudioSource AncenisContr3_2;
    AudioSource Nantes;
    public float masterVolume;
    private int currentMonologueIndex = 0;
    List<AudioSource> monologueSources = new List<AudioSource>();
    void Start()
    {
        monologueSources.Add(Orleans);
        monologueSources.Add(Beaugency);
        monologueSources.Add(Blois);
        monologueSources.Add(Amboise);
        monologueSources.Add(Tours);
        monologueSources.Add(Saumur);
        monologueSources.Add(Angers);
        monologueSources.Add(Chalonnes);
        monologueSources.Add(AncenisHonnete);
        monologueSources.Add(AncenisContr1);
        monologueSources.Add(AncenisContr2);
        monologueSources.Add(AncenisContr3_1);
        monologueSources.Add(AncenisContr3_2);
        monologueSources.Add(Nantes);
        instance = this;
        for(int i = 0; i < monologueSources.Count; i++) {
            monologueSources[i] = transform.GetChild(i).GetComponent<AudioSource>();
            monologueSources[i].loop = false;
            monologueSources[i].volume = masterVolume;
        }
        
    }

    public void playMonologue(int index) {
        monologueSources[currentMonologueIndex].Stop();
        monologueSources[index].Play();
        currentMonologueIndex = index;
    }
    public void stopMonologue() {
        monologueSources[currentMonologueIndex].Stop();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
