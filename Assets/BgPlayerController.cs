using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgPlayerController : MonoBehaviour
{
    [Header("PlayerInformation")]
    public Rigidbody rb;
    public Animator anim;

    public List<Transform> TransformAnimList;
    void DisableAllCharacter()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        DisableAllCharacter();
        transform.GetChild(PlayerPrefs.GetInt("CurrentCharacterNum")).gameObject.SetActive(true);
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                anim = transform.GetChild(i).GetComponent<Animator>();
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            TransformAnimList.Add(transform.GetChild(i).GetComponent<Transform>());
        }
        // StartCoroutine(delayStartCode());
        //transform.position = Vector3.MoveTowards(transform.position, Points[pointIndex].transform.position, moveSpeed *Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
