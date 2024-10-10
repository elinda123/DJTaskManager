using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMusic : MonoBehaviour
{
    Image image;

    void Start()
    {
        // audioSource = GetComponent<AudioSource>();
        image = BeatCounter.Instance.GetComponent<Image>();
    }

    public void Play()
    {
        if (BeatCounter.Instance.roundedTime == BeatCounter.Instance.maxCount)
        {
            RequestManager.Instance.isSync = true;
        }
        else
        {
            RequestManager.Instance.isSync = false;
        }

        if (InteractManager.Instance.melodyHolder.placedCards.Count > 0)
        {
            Melody lastMelody = (Melody)InteractManager.Instance.melodyHolder.placedCards[InteractManager.Instance.melodyHolder.placedCards.Count - 1];
            MusicManager.Instance.melodyBox.clip = lastMelody.audio;
            MusicManager.Instance.melodyBox.Play();

            MusicManager.Instance.currentMelody = lastMelody;
        }
        else if (InteractManager.Instance.melodyHolder.placedCards.Count == 0)
        {
            MusicManager.Instance.melodyBox.Stop();

            MusicManager.Instance.currentMelody = null;
        }

        if (InteractManager.Instance.beatHolder.placedCards.Count > 0)
        {
            Beat lastBeat = (Beat)InteractManager.Instance.beatHolder.placedCards[InteractManager.Instance.beatHolder.placedCards.Count - 1];
            MusicManager.Instance.beatBox.clip = lastBeat.audio;
            MusicManager.Instance.beatBox.Play();

            MusicManager.Instance.currentBeat = lastBeat;
        }
        else if (InteractManager.Instance.beatHolder.placedCards.Count == 0)
        {
            MusicManager.Instance.beatBox.Stop();

            MusicManager.Instance.currentBeat = null;
        }

        if (InteractManager.Instance.fxHolder.placedCards.Count > 0)
        {
            for (int i = 0; i <= InteractManager.Instance.fxHolder.placedCards.Count - 1; i++)
            {
                FX lastFX =(FX)InteractManager.Instance.fxHolder.placedCards[i];

                if (lastFX.specific == "High Pass")
                {
                    MusicManager.Instance.audioHighPassFilter.enabled = true;
                }
                else if (lastFX.specific == "Low Pass")
                {
                    MusicManager.Instance.audioLowPassFilter.enabled = true;
                }
                else if (lastFX.specific == "Reverb")
                {
                    MusicManager.Instance.audioReverbFilter.enabled = true;
                }

                MusicManager.Instance.currentFX[i] = lastFX;
            }
        }
        else if (InteractManager.Instance.fxHolder.placedCards.Count == 0)
        {
            for (int i = 0; i <= 1; i++)
            {
                MusicManager.Instance.fxBoxes[i].Stop();

                MusicManager.Instance.currentFX[i] = null;
            }

            MusicManager.Instance.audioHighPassFilter.enabled = false;
            MusicManager.Instance.audioLowPassFilter.enabled = false;
            MusicManager.Instance.audioReverbFilter.enabled = false;
        }
    }
}
