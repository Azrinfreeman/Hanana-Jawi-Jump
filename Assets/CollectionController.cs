using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectionController : MonoBehaviour
{
    public static CollectionController instance;

    void Awake()
    {
        instance = this;
    }

    public int stars;
    public TextMeshProUGUI textStar;
    public int rounds;
    public TextMeshProUGUI textRound;

    //public int health;
    public int roundsTotal;
    public int starsTotal;
    // Start is called before the first frame update
    void Start()
    {
        stars = PlayerPrefs.GetInt("StarsCollected_" + PlayerPrefs.GetInt("CurrentPlayerNo_"));
        roundsTotal = PlayerPrefs.GetInt("RoundsCollected_" + PlayerPrefs.GetInt("CurrentPlayerNo_"));

        textStar = transform
            .GetChild(0)
            .GetChild(0)
            .GetChild(0)
            .GetChild(0)
            .GetComponent<TextMeshProUGUI>();

        textRound = transform
            .GetChild(1)
            .GetChild(0)
            .GetChild(0)
            .GetChild(0)
            .GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        stars = PlayerPrefs.GetInt("StarsCollected_" + PlayerPrefs.GetInt("CurrentPlayerNo_"));
        roundsTotal = PlayerPrefs.GetInt("RoundsCollected_" + PlayerPrefs.GetInt("CurrentPlayerNo_"));
        textStar.text = stars.ToString();
        textRound.text = roundsTotal.ToString();
    }
    public void addStars(int star)
    {
        StartCoroutine(starsAdding(star));
    }

    IEnumerator starsAdding(int star)
    {
        stars += star;
        PlayerPrefs.SetInt("StarsCollected_" + PlayerPrefs.GetInt("CurrentPlayerNo_"), stars);
        GameController.instance.moneyTransform.GetComponent<Animator>().Play("collected");
        yield return new WaitForSeconds(0.2f);
    }
    public void addRounds(int round)
    {
        roundsTotal += round;
        PlayerPrefs.SetInt("RoundsCollected_" + PlayerPrefs.GetInt("CurrentPlayerNo_"), roundsTotal);
    }
}
