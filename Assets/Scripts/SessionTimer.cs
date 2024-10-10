using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SessionTimer : MonoBehaviour
{
    public TMP_Text textTime;
    public float totalTime;
    public int roundedTime;
    public bool isEnded = false;

    public GameObject endScreen;
    public TMP_Text endText;


    // Start is called before the first frame update
    void Start()
    {
        endScreen.SetActive(false);
        endText = endScreen.transform.Find("Text").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnded)
        {
            totalTime -= Time.deltaTime;
            roundedTime = Mathf.FloorToInt(totalTime + 1);
            textTime.text = roundedTime.ToString();

            if (totalTime <= 0)
            {
                totalTime = 0;
                EndSession();
            }
        }
        
    }

    void EndSession()
    {
        float successPercent = (float)SuccessBar.Instance.currentPoints / (float)(SuccessBar.Instance.maxLayer * (float)SuccessBar.Instance.maxPointsPerLayer - 1);
        if (successPercent >= 0.8f)
        {
            endText.text = "Success";
        }
        else
        {
            endText.text = "Failed";
        }
        Time.timeScale = 0;
        endScreen.SetActive(true);
        isEnded = true;
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // LoadScene.Instance.LoadingScene(SceneManager.GetActiveScene().name);
    }
}
