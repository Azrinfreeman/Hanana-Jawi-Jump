using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionController : MonoBehaviour
{
    public Transform selectedTransform;
    public Transform players;

    public Image bgName;
    // Start is called before the first frame update
    void Start()
    {
        bgName = GameObject.Find("bgName").GetComponent<Image>();
        selectedTransform = transform.GetChild(0).transform;
        players = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < AnimationController.instance.players.Count; i++)
        {
            if (players.GetChild(i).transform.gameObject.activeSelf)
            {

                if (AnimationController.instance.players[i].GetSiblingIndex() == PlayerPrefs.GetInt("CurrentCharacterNum"))
                {
                    //                    Debug.Log(AnimationController.instance.players[i].GetSiblingIndex());
                    selectedTransform.gameObject.SetActive(true);
                    bgName.color = new Color32(144, 0, 255, 100);
                }
                else
                {
                    bgName.color = new Color32(0, 255, 17, 100);
                    selectedTransform.gameObject.SetActive(false);
                }
            }

        }
    }
}
