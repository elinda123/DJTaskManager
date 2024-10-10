using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static LoadScene Instance {get; set;}
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



    public GameObject imageLeft;
    public GameObject imageRight;

    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        DontDestroyOnLoad(gameObject);
        imageLeft = gameObject.transform.Find("Image 1").gameObject;
        imageRight = gameObject.transform.Find("Image 2").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadingScene(SceneManager.GetActiveScene().name);
        }
    }

    public void LoadingScene(String name)
    {
        StartCoroutine(LoadSceneWithTransition(name));
    }

    public IEnumerator LoadSceneWithTransition(string sceneName)
    {
        // Start moving the images
        yield return StartCoroutine(Transition());

        // After the animation, load the scene
        SceneManager.LoadScene(sceneName);

        // Wait for one frame after loading the scene
        yield return null;

        // Re-assign the camera in the new scene
        canvas.worldCamera = Camera.main;
    }   

    IEnumerator Transition()
    {
        Vector3 startPosition1 = new Vector3(-Screen.width, 0, 0);
        Vector3 endPosition1 = new Vector3(-Screen.width / 2, 0, 0);

        Vector3 startPosition2 = new Vector3(Screen.width, 0, 0);
        Vector3 endPosition2 = new Vector3(Screen.width / 2, 0, 0);

        float elapsedTime = 0f;
        float duration = 1.5f;

        while (elapsedTime < duration)
        {
            imageLeft.transform.position = Vector3.Lerp(startPosition1, endPosition1, elapsedTime / duration);
            imageRight.transform.position = Vector3.Lerp(startPosition2, endPosition2, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageLeft.transform.position = startPosition1;
        imageRight.transform.position = startPosition2;
    }
}
