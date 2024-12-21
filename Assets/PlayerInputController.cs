using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInputController : MonoBehaviour
{
    public TMP_InputField textInput;


    // Start is called before the first frame update
    void Start()
    {
        textInput = transform.Find("content").transform.GetChild(0).transform.GetChild(1).GetComponent<TMP_InputField>();

        if (PlayerPrefs.GetInt("FirstTime") == 1)
        {
            transform.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator submit()
    {

        using (
            UnityWebRequest www = UnityWebRequest.Get(
                "https://hananaelearning.com/jawimodul2/fetchMark.php?name=" + textInput.text
            )
        )
        {
            yield return www.SendWebRequest();
            if (
                www.result == UnityWebRequest.Result.ConnectionError
                || www.result == UnityWebRequest.Result.ProtocolError
            )
            {
                Debug.Log(www.error);
                Debug.Log("error server");
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);

                if (www.downloadHandler.text.Equals("Valid"))
                {

                    GetComponent<Animator>().Play("dismiss");
                    GameStartupController.instance.SaveNewPlayerName(textInput.text);
                    GameStartupController.instance.SetAsCurrentPlayer(textInput.text);

                    PlayerPrefs.SetInt("FirstTime", 1);
                    GameStartupController.instance.ApplyScoreAgain();

                    textInput.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "*Valid Name";
                    textInput.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.green;
                    CurrentPlayerName.instance.ApplyName();
                }
                else
                {
                    textInput.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "*Invalid Name";
                    textInput.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.red;
                }
            }
        }
    }

    public void SubmitName()
    {
        if (!String.IsNullOrEmpty(textInput.text))
        {
            StartCoroutine(submit());
        }
        else
        {
            textInput.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "*Insert name";
            textInput.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.red;
        }

    }

    public void DisableGameObject()
    {
        transform.gameObject.SetActive(false);
    }
}
