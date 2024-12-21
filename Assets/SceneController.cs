using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [Header("LoadingScreen")]
    public Transform LoadingScreen;
    

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

    // Start is called before the first frame update
    void Start()
    {
        //LoadingScreen = transform.GetChild(11).GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() { }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void FinishSplash()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadScene(string name)
    {
        StartCoroutine(LoadAsyncScene(name));
    }

    IEnumerator loadingScene(string name, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
