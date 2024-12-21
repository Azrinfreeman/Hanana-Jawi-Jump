using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class onHover : MonoBehaviour
{
    public bool isOff;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("badges_lvl" + transform.name) == 1)
        {
            transform.GetComponent<Button>().onClick.AddListener(() => ShowHighScore());
        }
    }

    // Update is called once per frame
    void Update() { }

    public void ShowHighScore()
    {
        if (!isOff)
        {
            transform.Find("hover").gameObject.SetActive(true);
            isOff = true;
        }
        else if (isOff)
        {
            transform.Find("hover").gameObject.SetActive(false);
            isOff = false;
        }
    }
}
