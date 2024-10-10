using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance {get; set;}
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public AudioSource melodyBox;
    public AudioSource beatBox;
    public AudioSource[] fxBoxes;
    
    public Melody currentMelody;
    public Beat currentBeat;
    public FX[] currentFX;

    public AudioLowPassFilter audioLowPassFilter;
    public AudioHighPassFilter audioHighPassFilter;
    public AudioReverbFilter audioReverbFilter;

    public void PauseAll()
    {
        melodyBox.Pause();
        beatBox.Pause();
        for (int i = 0; i < currentFX.Length; i++)
        {
            fxBoxes[i].Pause();
        }
        
    }
    public void UnPauseAll()
    {
        melodyBox.UnPause();
        beatBox.UnPause();
        for (int i = 0; i < currentFX.Length; i++)
        {
            fxBoxes[i].UnPause();
        }
    }

}
