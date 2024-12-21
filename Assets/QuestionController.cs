using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionController : MonoBehaviour
{
    public static QuestionController instance;

    void Awake()
    {
        instance = this;

        for (int i = 0; i < GameObject.Find("QuestionsList").transform.childCount; i++)
        {
            questionsList.Add(GameObject.Find("QuestionsList").transform.GetChild(i));
        }
    }

    public List<Transform> questionsList;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}
