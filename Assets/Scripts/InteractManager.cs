using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public static InteractManager Instance {get; set;}
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

    public InventoryManager cardInventory;
    public MixerHolder melodyHolder;
    public MixerHolder beatHolder;
    public MixerHolder fxHolder;
}
