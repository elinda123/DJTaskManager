using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public string slotType;

    public void OnDrop(PointerEventData eventData)
    {
        DraggableCard draggedCard = eventData.pointerDrag.GetComponent<DraggableCard>();

        if (draggedCard != null)
        {
            RectTransform rectTransform = draggedCard.GetComponent<RectTransform>();
            CardButton cardButton = draggedCard.GetComponent<CardButton>();
            if (cardButton != null)
            {
                ScriptableObject card = cardButton.card;

                if (card is Melody melodyCard)
                {
                    if (slotType == melodyCard.cardType && GetComponent<MixerHolder>().placedCards.Count < GetComponent<MixerHolder>().maxCardNum)
                    {
                        draggedCard.transform.SetParent(transform);
                        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                        rectTransform.anchoredPosition = new Vector2(0f, 0f);
                        GetComponent<MixerHolder>().PlaceCard(melodyCard);
                        InventoryManager.Instance.RemoveCard(melodyCard);
                    }
                    else if (slotType == melodyCard.cardType + " Inventory" && transform != draggedCard.transform.parent)
                    {
                        draggedCard.transform.parent.GetComponent<MixerHolder>().ReturnCard(melodyCard);
                        InventoryManager.Instance.AddCard(melodyCard);
                        draggedCard.transform.SetParent(transform);
                    }
                }
                else if(card is Beat beatCard)
                {
                    if (slotType == beatCard.cardType && GetComponent<MixerHolder>().placedCards.Count < GetComponent<MixerHolder>().maxCardNum)
                    {
                        draggedCard.transform.SetParent(transform);
                        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                        rectTransform.anchoredPosition = new Vector2(0f, 0f);
                        GetComponent<MixerHolder>().PlaceCard(beatCard);
                        InventoryManager.Instance.RemoveCard(beatCard);
                    }
                    else if (slotType == beatCard.cardType + " Inventory" && transform != draggedCard.transform.parent)
                    {
                        draggedCard.transform.parent.GetComponent<MixerHolder>().ReturnCard(beatCard);
                        InventoryManager.Instance.AddCard(beatCard);
                        draggedCard.transform.SetParent(transform);
                    }
                }
                else if(card is FX fxCard)
                {
                    if (slotType == fxCard.cardType && GetComponent<MixerHolder>().placedCards.Count < GetComponent<MixerHolder>().maxCardNum)
                    {
                        draggedCard.transform.SetParent(transform);
                        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                        rectTransform.anchoredPosition = new Vector2(0f, 0f);
                        GetComponent<MixerHolder>().PlaceCard(fxCard);
                        InventoryManager.Instance.RemoveCard(fxCard);
                    }
                    else if (slotType == fxCard.cardType + " Inventory" && transform != draggedCard.transform.parent)
                    {
                        draggedCard.transform.parent.GetComponent<MixerHolder>().ReturnCard(fxCard);
                        InventoryManager.Instance.AddCard(fxCard);
                        draggedCard.transform.SetParent(transform);
                    }
                }
            }
        }
    }
}
