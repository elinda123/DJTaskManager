using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audiences : MonoBehaviour
{
    public int randomCooldown;
    public float timer;
    public bool canRequest = true;
    

    void Start()
    {
        randomCooldown = Random.Range(2, 6);
    }


    void Update()
    {
        if (canRequest)
        {
            timer += Time.deltaTime;
            if (timer >= randomCooldown)
            {
                timer = 0;
                randomCooldown = Random.Range(2, 6);
                canRequest = false;
                RandomRequest();
            }
        }
    }

    private void RandomRequest()
    {
        int randomNum = Random.Range(1, 3);
        List<Transform> audienceList = new List<Transform>();
        foreach (Transform child in gameObject.transform)
        {
            audienceList.Add(child);
        }

        List<Transform> selectedAudiences = new List<Transform>();
        for (int i = 0; i < randomNum; i++)
        {
            int randomIndex = Random.Range(0, audienceList.Count);
            Transform selected = audienceList[randomIndex];
            selectedAudiences.Add(selected);
            audienceList.RemoveAt(randomIndex);
        }

        for (int i = 0; i < selectedAudiences.Count; i++)
        {
            // selectedAudiences[i].GetComponent<Request>().canRequest = true;
            StartCoroutine(AppearAndMove(selectedAudiences[i]));
        }
    }

    IEnumerator AppearAndMove(Transform audience)
    {
        Vector3 startPosition = audience.position + new Vector3(0, -2f, 0);
        Vector3 endPosition = audience.position;

        Color startColor = new Color(1f, 1f, 1f, 0f);
        Color endColor = new Color(1f, 1f, 1f, 1f);

        Image spriteRenderer = audience.GetComponent<Image>();

        float elapsedTime = 0f;
        float duration = 1.5f; // Thời gian xuất hiện

        audience.position = startPosition;
        audience.gameObject.SetActive(true);

        while (elapsedTime < duration)
        {
            audience.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            spriteRenderer.color = Color.Lerp(startColor, endColor, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audience.position = endPosition;
        spriteRenderer.color = endColor;

        audience.GetComponent<Request>().canRequest = true;
    }
}
