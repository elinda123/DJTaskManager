using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public GameObject pauseScreen;

    void Start()
    {
        CloseAllScreen();
    }

    public void Pause()
    {
        MusicManager.Instance.PauseAll();
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        MusicManager.Instance.UnPauseAll();
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void CloseAllScreen()
    {
        pauseScreen.SetActive(false);
    }

}
