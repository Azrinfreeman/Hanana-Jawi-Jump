using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public List<String> AnimationName;

    public Animator anim;

    string current_animation;

    public int index;

    void Awake()
    {
        index = 0;
        anim = GameObject.Find("Player").transform.GetChild(0).GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (
            anim.GetCurrentAnimatorStateInfo(0).IsName(AnimationName[index])
            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f
        )
        {
            Debug.Log(AnimationName[index]);
            anim.Play(AnimationName[index]);

            current_animation = AnimationName[index];
        }

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = current_animation;
    }

    // Update is called once per frame
    void Update()
    {
        anim = GameObject.Find("Player").transform.GetChild(0).GetComponent<Animator>();
    }

    public void CycleAnimationButton()
    {
        index++;

        if (index != AnimationName.Count)
        {
            {
                Debug.Log(AnimationName[index]);
                anim.Play(AnimationName[index]);

                current_animation = AnimationName[index];
            }

            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = current_animation;
        }
        else
        {
            index = 0;
        }
    }

    public void CycleAnimationButtonLeft()
    {
        index--;

        if (index != -1)
        {
            {
                Debug.Log(AnimationName[index]);
                anim.Play(AnimationName[index]);

                current_animation = AnimationName[index];
            }

            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = current_animation;
        }
        else
        {
            index = 4;
        }
    }
}
