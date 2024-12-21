using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetBadges : MonoBehaviour
{
    public int level;

    // Start is called before the first frame update
    void Start()
    {
        level = int.Parse(SceneManager.GetActiveScene().name);

        transform
            .GetChild(0)
            .transform.GetChild(0)
            .gameObject.GetComponent<Button>()
            .onClick.AddListener(() => SetLevelBadgesSuccess());
        transform
            .GetChild(1)
            .transform.GetChild(0)
            .gameObject.GetComponent<Button>()
            .onClick.AddListener(() => SetLevelBadgesSuccess());
        ///



        transform
            .GetChild(0)
            .transform.GetChild(0)
            .gameObject.GetComponent<Button>()
            .onClick.AddListener(() => SetCoinHighScore());
        transform
            .GetChild(1)
            .transform.GetChild(0)
            .gameObject.GetComponent<Button>()
            .onClick.AddListener(() => SetCoinHighScore());
    }

    public void SetLevelBadgesSuccess()
    {
        PlayerPrefs.SetInt("badges_lvl" + level, 1);
    }

    public void SetCoinHighScore()
    {
        if (CollectionController.instance.stars > PlayerPrefs.GetInt("star_lvl" + level))
        {
            PlayerPrefs.SetInt("star_lvl" + level, CollectionController.instance.stars);
        }

        if (CollectionController.instance.rounds > PlayerPrefs.GetInt("coin_lvl" + level))
        {
            PlayerPrefs.SetInt("coin_lvl" + level, CollectionController.instance.rounds);
        }
    }

    // Update is called once per frame
    void Update() { }
}
