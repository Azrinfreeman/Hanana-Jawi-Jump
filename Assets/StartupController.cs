using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupController : MonoBehaviour
{
    // Start is called before the first frame update

    public int maxCharacters;
    void Start()
    {
        if (PlayerPrefs.GetInt("Character_No_0") == 0)
        {
            PlayerPrefs.SetInt("MaxCharacters", maxCharacters);

            Debug.Log("true");
            for (int i = 0; i < 10; i++)
            {
                if (i == 0)
                {

                    PlayerPrefs.SetInt("Character_No_" + i, 1);

                }
                else
                    PlayerPrefs.SetInt("Character_No_" + i, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
