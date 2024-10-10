using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class RequestCheck : MonoBehaviour
{
    public int requestID;
    public ScriptableObject requestData;
    public TMP_Text text;


    void Update()
    {
        if (MusicManager.Instance.currentMelody == requestData ||
            MusicManager.Instance.currentBeat == requestData ||
            MusicManager.Instance.currentFX[0] == requestData ||
            MusicManager.Instance.currentFX[1] == requestData)
        {
            transform.parent.parent.GetComponent<Request>().requestNum -= 1;
            transform.parent.parent.GetComponent<Request>().successCount += 1;
            DestroyImmediate(gameObject);
        }

        text.text = requestData.name;
    }
}
