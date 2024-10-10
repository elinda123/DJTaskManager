using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MelodyHolder : MonoBehaviour
{
    public GameObject cardPrefab;
    public float spacing = 150f;
    public Vector3 startPosition = new Vector3(0, 0, 0);
    public int maxCardNum = 1;

    public List<ScriptableObject> placedCards = new List<ScriptableObject>();

    public void PlaceCard(ScriptableObject card)
    {
        if (!placedCards.Contains(card))
        {
            placedCards.Add(card);
            SpawnCards();
        }
    }

    public void ReturnCard(ScriptableObject card)
    {
        if (placedCards.Contains(card))
        {
            placedCards.Remove(card);
            SpawnCards();
        }
    }

    private void SpawnCards()
    {
        ClearExistingCards();

        for (int i = 0; i < placedCards.Count; i++)
        {
            ScriptableObject card = placedCards[i];
            CreateCard(card, i);
        }
    }

    private void ClearExistingCards()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void CreateCard(ScriptableObject card, int index)
    {
        GameObject newCard = Instantiate(cardPrefab, transform);
        newCard.transform.localPosition = startPosition + new Vector3(index * spacing, 0, 0);

        Button button = newCard.GetComponent<Button>();
        button.onClick.RemoveAllListeners();

        if (card is Melody melodyCard)
        {
            // button.onClick.AddListener(() => InteractManager.Instance.OnCardClicked(melodyCard, melodyCard.cardType));
            SetCardUI(newCard, melodyCard.image, melodyCard.name, card);
        }
        else if (card is Beat beatCard)
        {
            // button.onClick.AddListener(() => InteractManager.Instance.OnCardClicked(beatCard, beatCard.cardType));
            SetCardUI(newCard, beatCard.image, beatCard.name, card);
        }
        else if (card is FX fxCard)
        {
            // button.onClick.AddListener(() => InteractManager.Instance.OnCardClicked(fxCard, fxCard.cardType));
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
