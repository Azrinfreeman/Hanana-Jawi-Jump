using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GetScores : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Star: "+ PlayerPrefs.GetInt("star_lvl"+transform.parent.name);
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Coin: "+ PlayerPrefs.GetInt("coin_lvl"+transform.parent.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
