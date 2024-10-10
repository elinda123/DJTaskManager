using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestManager : MonoBehaviour
{
    public IconDatabase iconDatabase;
    public GameObject iconPrefab;

    public List<ScriptableObject> cardDatabase;

    public bool isSync = false;

    public static RequestManager Instance {get; set;}
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
    
}
