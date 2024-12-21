using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BadgeController : MonoBehaviour
{
    public List<Transform> badges;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int l = 0; l < transform.GetChild(i).transform.childCount; l++)
            {
                badges.Add(
                    transform.GetChild(i).transform.GetChild(l).transform.GetComponent<Transform>()
                );
            }
        }
    }

    public void UnlockBadges()
    {
        for (int i = 0; i < badges.Count; i++)
        {
            if (PlayerPrefs.GetInt("badges_lvl" + (i+1)) == 1)
            {
                badges[i]
                    .transform.GetChild(0)
                    .transform.GetChild(0)
                    .transform.gameObject.SetActive(false);
                badges[i].transform.GetChild(0).transform.gameObject.GetComponent<Image>().color =
                    new Color32(255, 255, 255, 255);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //badges_lvl
        UnlockBadges();
    }
}
