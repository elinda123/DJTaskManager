using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Mood : MonoBehaviour
{
    public enum PatienceState
    {
        Happy,       // 100-50
        Mellow,      // 50-26
        Angry        // 25-0
    }

    public Image patienceBar;
    public PatienceState state;
    public float maxPatience = 100f;
    public float currentPatience;
    public float decayRate = 5f;
    public TMP_Text stateText;


    public int rewardHappy = 10;
    public int rewardMellow = 8;
    public int rewardAngry = 5;

    private Request request;
    void Start()
    {
        request = GetComponent<Request>();
        currentPatience = maxPatience;
        UpdatePatienceUI();
        stateText.enabled = false;
    }


    void Update()
    {
        if (request.canPatient)
        {
            currentPatience -= Time.deltaTime * decayRate;
            if (currentPatience < 0)
            {
                currentPatience = 0;
                request.DeleteAllIcon(gameObject.transform.Find("Request"));
                request.EndRequest(false);
            }
            if (request.requestNum <= 0)
            {
                request.EndRequest(true);
            }

            UpdatePatienceState();
            UpdatePatienceUI();
            UpdatePatienceBarColor();
        }
    }

    void UpdatePatienceState()
    {
        if (currentPatience >= 51)
        {
            state = PatienceState.Happy;
            stateText.color = Color.green;
        }
        else if (currentPatience >= 26)
        {
            state = PatienceState.Mellow;
            stateText.color = Color.yellow;
        }
        else
        {
            state = PatienceState.Angry;
            stateText.color = Color.red;
        }

        stateText.text = state.ToString();
    }

    void UpdatePatienceUI()
    {
        patienceBar.fillAmount = currentPatience / maxPatience;
    }

    void UpdatePatienceBarColor()
    {
        float patiencePercent = currentPatience / maxPatience;

        if (patiencePercent > 0.5f)
        {
            // Happy
            patienceBar.color = Color.green;
        }
        else if (patiencePercent > 0.25f)
        {
            // Mellow
            patienceBar.color = Color.yellow;
        }
        else
        {
            // Angry
            patienceBar.color = Color.red;
        }

        if (patiencePercent <= 0.25f)
        {
            float blinkSpeed = Mathf.PingPong(Time.time * 2, 1);
            patienceBar.color = Color.Lerp(Color.red, Color.black, blinkSpeed);
        }
    }

    public int CompleteTask()
    {
        int reward = 0;
        switch (state)
        {
            case PatienceState.Happy:
                reward = rewardHappy;
                break;
            case PatienceState.Mellow:
                reward = rewardMellow;
                break;
            case PatienceState.Angry:
                reward = rewardAngry;
                break;
        }

        if (RequestManager.Instance.isSync == false)
        {
            reward -= 5;
        }

        return reward;
    }
}
