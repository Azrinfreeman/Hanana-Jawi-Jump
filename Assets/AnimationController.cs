using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{

    public List<Transform> players;
    public List<String> animName;
    public int listNum;

    public int listAnimNum;
    public static AnimationController instance;

    public TextMeshProUGUI textName;
    public TextMeshProUGUI textAnimName;

    public Transform ButtonPilih;

    [Header("Needed")]
    public int coinsNeeded;
    public int trophyNeeded;

    public TextMeshProUGUI coinsNeededText;
    public TextMeshProUGUI trophyNeededText;

    public Transform lockImage;

    public Transform warning;

    void Awake()
    {
        instance = this;

    }
    IEnumerator characterBuying()
    {
        //ButtonPilih.GetComponent<Animator>().SetTrigger("onClick");
        ButtonPilih.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        if (PlayerPrefs.GetInt("StarsCollected_" + PlayerPrefs.GetInt("CurrentPlayerNo_")) >= coinsNeeded &&
         PlayerPrefs.GetInt("RoundsCollected_" + PlayerPrefs.GetInt("CurrentPlayerNo_")) >= trophyNeeded)
        {
            Debug.Log("Unlock character");

            //unlock character
            PlayerPrefs.SetInt("Character_No_" + listNum, 1);
            warning.gameObject.SetActive(false);
            //minus the coins from player
            int sum = PlayerPrefs.GetInt("StarsCollected_" + PlayerPrefs.GetInt("CurrentPlayerNo_"));
            sum -= coinsNeeded;

            PlayerPrefs.SetInt("StarsCollected_" + PlayerPrefs.GetInt("CurrentPlayerNo_"), sum);

            lockImage.GetComponent<Animator>().Play("lockDisappear");
            yield return new WaitForSeconds(1);
            //BeliPrompt.gameObject.SetActive(true);
            if (!GameObject.Find("unlocked").transform.GetComponent<AudioSource>().isPlaying)
            {
                GameObject.Find("unlocked").transform.GetComponent<AudioSource>().Play();
            }
            yield return new WaitForSeconds(1.5f);
            ReassignImageandName();

        }
        else
        {
            Debug.Log("Tak cukup duit");
            warning.gameObject.SetActive(true);
        }

    }

    // Start is called before the first frame update
    void Start()
    {

        warning = GameObject.Find("warning").transform;
        warning.gameObject.SetActive(false);
        for (int i = 0; i < transform.childCount; i++)
        {
            players.Add(transform.GetChild(i));
        }
        textName = GameObject.Find("name").GetComponent<TextMeshProUGUI>();
        // textAnimName = GameObject.Find("animationName").GetComponent<TextMeshProUGUI>();
        textName.text = transform.GetChild(listNum).GetComponent<CharacterInfo>().names;

        transform.GetChild(listNum).GetComponent<Animator>().Play(animName[listAnimNum]);
        //textAnimName.text = animName[listAnimNum];
        ButtonPilih = GameObject.Find("ButtonPilih").transform;

        coinsNeededText = GameObject.Find("Coins").transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        trophyNeededText = GameObject.Find("Stars").transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        lockImage = GameObject.Find("Lock").GetComponent<Transform>();

        ReassignImageandName();
    }

    // Update is called once per frame
    void Update()
    { }
    void DisableAllCharacter()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void chooseCharacter()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {

                PlayerPrefs.SetInt("CurrentCharacterNum", i);
            }
        }
    }
    public void CycleAnimationButton()
    {
        lockImage.gameObject.SetActive(true);
        warning.gameObject.SetActive(false);
        listNum++;
        if (listNum == players.Count)
        {
            listNum = 0;
        }
        ReassignImageandName();
        transform.GetChild(listNum).GetComponent<Animator>().Play(animName[listAnimNum]);
        //   textAnimName.text = animName[listAnimNum];
    }

    public void cycleAnimation()
    {
        transform.GetChild(listNum).GetComponent<Animator>().Play("Idle");
        listAnimNum++;
        if (listAnimNum == animName.Count)
        {
            listAnimNum = 0;
        }

        transform.GetChild(listNum).GetComponent<Animator>().Play(animName[listAnimNum]);
        //    textAnimName.text = animName[listAnimNum];
    }

    public void cycleAnimationBack()
    {

        transform.GetChild(listNum).GetComponent<Animator>().Play("Idle");
        listAnimNum--;

        if (listAnimNum < 0)
        {
            listAnimNum = animName.Count - 1;
        }
        transform.GetChild(listNum).GetComponent<Animator>().Play(animName[listAnimNum]);
        //   textAnimName.text = animName[listAnimNum];
    }



    public void CycleAnimationButtonLeft()
    {
        warning.gameObject.SetActive(false);
        lockImage.gameObject.SetActive(true);
        listNum--;

        if (listNum < 0)
        {
            listNum = players.Count - 1;
        }
        ReassignImageandName();
        transform.GetChild(listNum).GetComponent<Animator>().Play(animName[listAnimNum]);
        //   textAnimName.text = animName[listAnimNum];
    }
    private void ReassignImageandName()
    {
        DisableAllCharacter();
        textName.text = transform.GetChild(listNum).GetComponent<CharacterInfo>().names;
        transform.GetChild(listNum).gameObject.SetActive(true);
        transform.GetChild(listNum).GetComponent<Animator>().Play("Idle");


        coinsNeeded = transform.GetChild(listNum).GetComponent<CharacterInfo>().coinsNeeded;
        coinsNeededText.text = coinsNeeded.ToString() + "\nCoins";

        trophyNeeded = transform.GetChild(listNum).GetComponent<CharacterInfo>().trophyNeeded;
        trophyNeededText.text = trophyNeeded.ToString() + "\nTrophy";

        if (PlayerPrefs.GetInt("Character_No_" + listNum) == 1)
        {

            ButtonPilih.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();

            ButtonPilih.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "PILIH";
            lockImage.gameObject.SetActive(false);
            ButtonPilih.GetChild(0).GetComponent<Button>().onClick.AddListener(() => ApplyCharacter());
        }
        else if (PlayerPrefs.GetInt("Character_No_" + listNum) == 0)
        {

            ButtonPilih.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();

            ButtonPilih.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "BELI";
            lockImage.gameObject.SetActive(true);
            ButtonPilih.GetChild(0).GetComponent<Button>().onClick.AddListener(() => BeliCharacter());
        }
    }
    public void ApplyCharacter()
    {

        PlayerPrefs.SetInt("CurrentCharacterNum", listNum);
    }
    public void BeliCharacter()
    {
        StartCoroutine(characterBuying());
    }
}
