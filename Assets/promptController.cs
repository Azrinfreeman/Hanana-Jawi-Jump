using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class promptController : MonoBehaviour
{
    public Transform LoadingScreen;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(
            transform.parent.GetChild(transform.childCount + 1).transform.GetComponent<Transform>()
        );
        LoadingScreen = transform
            .parent.GetChild(transform.childCount + 1)
            .transform.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() { }

    public void RedirectToLevel(int level)
    {
        StartCoroutine(LoadAsyncScene(level.ToString()));
    }

    IEnumerator LoadAsyncScene(string level)
    {
        LoadingScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.3f);
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void ChoosingLevel(string message)
    {
        //content
        int level = Int32.Parse(EventSystem.current.currentSelectedGameObject.name);

        transform
            .GetChild(1)
            .transform.GetChild(0)
            .transform.GetChild(0)
            .transform.GetChild(0)
            .GetComponent<TextMeshProUGUI>()
            .text = message + " " + level;

        transform
            .GetChild(1)
            .transform.GetChild(1)
            .transform.GetChild(0)
            .transform.GetChild(1)
            .GetComponent<Button>()
            .onClick.AddListener(() => RedirectToLevel(level));

        //display prompt

        transform.gameObject.SetActive(true);
    }
}
