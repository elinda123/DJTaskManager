using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance {get; set;}
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


    public GameObject cardPrefab;

    public List<Melody> melodies = new List<Melody>();
    public List<Beat> beats = new List<Beat>();
    public List<FX> fxs = new List<FX>();

    public List<ScriptableObject> inventory = new List<ScriptableObject>();

    public float spacing = 150f;
    public Vector3 startPosition = new Vector3(0, 0, 0);

    public RectTransform container;

    private void Start()
    {
        container.GetComponent<RectTransform>();
        inventory.AddRange(melodies);
        inventory.AddRange(beats);
        inventory.AddRange(fxs);
        SpawnCards();
    }

    public void AddCard(ScriptableObject newCard)
    {
        inventory.Add(newCard);
        SpawnCards();
    }
    
    public void RemoveCard(ScriptableObject cardToRemove)
    {
        inventory.Remove(cardToRemove);
        SpawnCards();
    }

    public void SpawnCards()
    {
        ClearAllChildren();

        for (int i = 0; i < inventory.Count; i++)
        {
            ScriptableObject card = inventory[i];
            if (card is Melody melodyCard)
            {
                CreateCard(melodyCard, "Melody Container", i);
            }
            else if (card is Beat beatCard)
            {
                CreateCard(beatCard, "Beat Container", i);
            }
            else if (card is FX fxCard)
            {
                CreateCard(fxCard, "FX Container", i);
            }
        }
    }

    private void ClearAllChildren()
    {
        foreach (Transform child in transform)
        {
            foreach (Transform grandChild in child)
            {
                Destroy(grandChild.gameObject);
            }
        }
    }

    private void CreateCard(ScriptableObject card, string containerName, int index)
    {
        Transform container = gameObject.transform.Find(containerName);
        GameObject newCard = Instantiate(cardPrefab, container);
        newCard.transform.localPosition = startPosition + new Vector3(index * spacing, 0, 0);

        Button button = newCard.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        
        if (card is Melody melodyCard)
        {
            SetCardUI(newCard, melodyCard.image, melodyCard.name, card);
        }
        else if (card is Beat beatCard)
        {
            SetCardUI(newCard, beatCard.image, beatCard.name, card);
        }
        else if (card is FX fxCard)
        {
            SetCardUI(newCard, fxCard.image, fxCard.name, card);
        }
    }

    private void SetCardUI(GameObject cardObj, Sprite image, string cardName, ScriptableObject card)
    {
        Image backImage = cardObj.transform.Find("Image").GetComponent<Image>();
        if (backImage != null)
        {
            backImage.sprite = image;
        }

        TMP_Text nameText = cardObj.transform.Find("Name").GetComponent<TMP_Text>();
        if (nameText != null)
        {
            nameText.text = cardName;
        }

        CardButton cardButton = cardObj.GetComponent<CardButton>();
        if (cardButton != null)
        {
            cardButton.card = card;
        }
    }
}
