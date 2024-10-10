using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeatCounter : MonoBehaviour
{
    public static BeatCounter Instance {get; set;}
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

    public float timer;
    public float maxCount;
    public TMP_Text text;
    public int roundedTime;

    private Image[] childImages;
    private int previousRoundedTime;

    // Start is called before the first frame update
    void Start()
    {
        timer = 1f;
        childImages = new Image[4];


        for (int i = 1; i <= 4; i++)
        {
            Transform child = transform.Find(i.ToString());
            if (child != null)
            {
                childImages[i - 1] = child.GetComponent<Image>();
            }
        }

        previousRoundedTime = -1;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= maxCount + 1)
        {
            timer = 1;
        }

        roundedTime = Mathf.FloorToInt(timer);
        text.text = roundedTime.ToString();

        if (roundedTime != previousRoundedTime)
        {
            ChangeColor();
            previousRoundedTime = roundedTime;
        }
    }

    void ChangeColor()
    {
        
        for (int i = 0; i < childImages.Length; i++)
        {
            if (childImages[i] != null)
            {
                childImages[i].color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            }
        }


        for (int i = 0; i < roundedTime && i < childImages.Length; i++)
        {
            if (childImages[i] != null)
            {
                childImages[i].color = new Color(0f / 255f, 150f / 255f, 255f / 255f, 255f / 255f);
            }
        }
    }
}
