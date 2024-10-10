using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Request : MonoBehaviour
{
    public int randomCooldown;
    public float timer;
    public bool canRequest = true;
    public bool canPatient = false;
    public float spacing = 50f;
    public Vector3 startPosition = new Vector3(0, 0, 0);

    public Image patientBar;
    public Mood mood;

    public int requestNum;
    public int successCount;


    // Start is called before the first frame update
    void Start()
    {
        patientBar = gameObject.transform.Find("Patience Bar").GetComponent<Image>();
        mood = GetComponent<Mood>();
        patientBar.enabled = false;
        randomCooldown = Random.Range(5, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (canRequest)
        {
            canRequest = false;
            canPatient = true;
            mood.currentPatience = mood.maxPatience;
            patientBar.enabled = true;
            patientBar.fillAmount = 1;
            mood.stateText.enabled = true;
            TakeRequest();
        }
    }
    public void TakeRequest()
    {
        successCount = 0;

        string[] requestType = { "Melody", "Beat", "FX" };
        List<string> availableTypes = new List<string>(requestType);

        requestNum = Random.Range(1, 4);

        List<string> selectedRequests = new List<string>();

        for (int i = 0; i < requestNum; i++)
        {
            int randomIndex = Random.Range(0, availableTypes.Count);
            string selectedType = availableTypes[randomIndex];
            selectedRequests.Add(selectedType);
            availableTypes.RemoveAt(randomIndex);
        }


        for (int i = 0; i < selectedRequests.Count; i++)
        {
            string iconType = selectedRequests[i];
            GameObject newIcon = Instantiate(RequestManager.Instance.iconPrefab, gameObject.transform.Find("Request"));
            newIcon.transform.localPosition = startPosition + new Vector3(i * spacing, 0, 0);
            newIcon.GetComponent<Image>().sprite = RequestManager.Instance.iconDatabase.FindIconByName(iconType).image;
            newIcon.GetComponent<RequestCheck>().requestID = i + 1;
            newIcon.GetComponent<RequestCheck>().requestData = RandomSpecificData(iconType);
        }
    }

    public void DeleteAllIcon(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }

    public void EndRequest(bool isSuccess)
    {
        if (isSuccess)
        {
            patientBar.enabled = false;
            canPatient = false;
            mood.stateText.enabled = false;
            SuccessBar.Instance.AddPoints(GetComponent<Mood>().CompleteTask());

            transform.parent.GetComponent<Audiences>().canRequest = true;
        }
        else
        {
            patientBar.enabled = false;
            canPatient = false;
            mood.stateText.enabled = false;
            SuccessBar.Instance.SubtractPoints(5);

            transform.parent.GetComponent<Audiences>().canRequest = true;
        }

        gameObject.SetActive(false);
        
    }

    ScriptableObject RandomSpecificData(string selectedType)
    {
        List<ScriptableObject> listToRandom = new List<ScriptableObject>();

        if (selectedType == "Melody")
        {
            foreach (ScriptableObject card in RequestManager.Instance.cardDatabase)
            {
                if (card is Melody melodyCard && card != MusicManager.Instance.currentMelody)
                {
                    listToRandom.Add(melodyCard);
                }
            }
        }
        else if (selectedType == "Beat")
        {
            foreach (ScriptableObject card in RequestManager.Instance.cardDatabase)
            {
                if (card is Beat beatCard && card != MusicManager.Instance.currentBeat)
                {
                    listToRandom.Add(beatCard);
                }
            }
        }
        else if (selectedType == "FX")
        {
            foreach (ScriptableObject card in RequestManager.Instance.cardDatabase)
            {
                if (card is FX fxCard && card != MusicManager.Instance.currentFX[0])
                {
                    listToRandom.Add(fxCard);
                }
            }
        }

        int randomIndex = Random.Range(0, listToRandom.Count);
        return listToRandom[randomIndex];
        
    }
}
