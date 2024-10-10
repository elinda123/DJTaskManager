using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuccessBar : MonoBehaviour
{
    public static SuccessBar Instance {get; set;}
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


    public Image[] progressSlots;
    public Color[] layerColors;
    public int maxLayer = 8;
    public int maxPointsPerLayer = 10;
    public int currentPoints = 0;
    public int currentLayer = 0;
    public int currentLayerPoints = 0;

    public float timer;

    void Start()
    {
        maxLayer = layerColors.Length;
        UpdateProgressBar();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer = 0;
            AddPoints(1);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            SubtractPoints(1);
        }
    }

    public void AddPoints(int points)
    {
        currentPoints += points;
        if (currentPoints > maxLayer * maxPointsPerLayer - 1)
        {
            currentPoints = maxLayer * maxPointsPerLayer - 1;
        }

        currentLayerPoints = currentPoints % maxPointsPerLayer;
        
        if (currentPoints / maxPointsPerLayer > currentLayer)
        {
            currentLayer++;
        }
        UpdateProgressBar();
    }

    public void SubtractPoints(int points)
    {
        currentPoints -= points;
        if (currentPoints < 0)
        {
            currentPoints = 0;
        }

        currentLayerPoints = currentPoints % maxPointsPerLayer;

        if (currentPoints / maxPointsPerLayer < currentLayer && currentLayer > 0)
        {
            currentLayer--;
        }
        UpdateProgressBar();
    }

    void UpdateProgressBar()
    {
        for (int i = 0; i < progressSlots.Length; i++)
        {
            if (i <= currentLayerPoints)
            {
                progressSlots[i].GetComponent<Image>().color = layerColors[currentLayer];
            }
            else
            {
                if (currentLayer != 0)
                {
                    progressSlots[i].GetComponent<Image>().color = layerColors[currentLayer - 1];
                }
                else
                {
                    progressSlots[i].GetComponent<Image>().color = new Color(0, 0, 0, 255f);
                }
                
            }

        }
        if (currentLayerPoints == 0 && currentLayer != 0)
        {
            progressSlots[progressSlots.Length - 1].GetComponent<Image>().color = layerColors[currentLayer - 1];
        }
    }
}
