using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public System.Random rnd = new System.Random();
    public System.Random rnd2 = new System.Random();

    public System.Random rnd3 = new System.Random();
    public static GameController instance;
    public Transform FreeLookCamera;

    public Animator CameraAnim;

    public Button SettingBtn;

    void Awake()
    {
        instance = this;
    }

    [Header("PlayerCollections")]
    public Transform moneyTransform;
    public Transform roundTransform;

    public bool[] enterLevel;

    [Header("Questions")]
    public bool gameStart;
    public int level;
    public int questionCount;

    public TextMeshProUGUI levelText;
    public string questions;

    public int questionsNum;
    public int answers;

    public List<int> randomAnswer;

    public int operation;

    public bool isQuestionShowUp;

    [Header("EndScreenMenu")]
    public Transform Bg;
    public Transform endMenuScreen;

    public Transform endGameMenu;

    [Header("Buttons")]
    public List<Button> ansButton;

    private Button soundBtn;
    [Header("TimeToAnswer")]
    public Transform Timing;
    [Header("references")]
    public PathController pathController;
    public bool isDoneAnswer;

    IEnumerator collectedRound()
    {
        //disable the trigger for tripping
        if (pathController.isTriggerJump)
        {
            //jump collider
            ObstaclesController
                .instance.ObstaclesList[0]
                .transform.GetChild(0)
                .GetComponent<BoxCollider>()
                .enabled = true;
            //crash collider
            ObstaclesController
                .instance.ObstaclesList[0]
                .transform.GetChild(1)
                .GetComponent<BoxCollider>()
                .enabled = false;
        }
        //disable the trigger for tripping
        if (pathController.isTriggerEdge)
        {
            ObstaclesController
                .instance.TriggerJumpList[0]
                .transform.GetChild(1)
                .GetComponent<BoxCollider>()
                .enabled = false;
        }


        //collect stars and timer
        if (level < 4)
        {
            Debug.Log("stars2");
            if (TimeToAnswer.instance.maxTime > 8)
            {
                CollectionController.instance.addStars(10);
            }
            else if (TimeToAnswer.instance.maxTime > 4 && TimeToAnswer.instance.maxTime <= 8)
            {
                int t3;
                t3 = (int)TimeToAnswer.instance.maxTime;
                CollectionController.instance.addStars(t3);
            }
            else if (TimeToAnswer.instance.maxTime > 0 && TimeToAnswer.instance.maxTime <= 4)
            {
                CollectionController.instance.addStars(3);
            }

        }







        //Collect the round count

        CollectionController.instance.addRounds(1);
        roundTransform.GetComponent<Animator>().Play("collected");

        yield return new WaitForSeconds(0.45f);
        //play sound
        if (!GameObject.Find("collected").GetComponent<AudioSource>().isPlaying)
        {
            GameObject.Find("collected").GetComponent<AudioSource>().Play();
        }
        yield return new WaitForSeconds(0.25f);
        roundTransform.GetComponent<Animator>().Play("afterCollected");

        //destroy and remove the questions from the list
        QuestionController.instance.questionsList.RemoveAt(answers);
        Destroy(GameObject.Find("QuestionsList").transform.GetChild(answers).gameObject);
        //Destroy(gameObject);
    }

    IEnumerator collectedRound80()
    {
        speedUpTime();
        //add if else for time
        if (TimeRemainingObtain.instance.timeRemaining > 1f)
        {
            HideQuestionsButtons();
            pathController.timeToReach.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            //disable the trigger for tripping
            if (pathController.isTriggerJump)
            {
                //jump collider need to turn on
                ObstaclesController
                    .instance.ObstaclesList[0]
                    .transform.GetChild(0)
                    .GetComponent<BoxCollider>()
                    .enabled = true;

                //crash collider not needed actually
                ObstaclesController
                    .instance.ObstaclesList[0]
                    .transform.GetChild(1)
                    .GetComponent<BoxCollider>()
                    .enabled = false;
            }
            //disable the trigger for tripping
            if (pathController.isTriggerEdge)
            {
                ObstaclesController
                    .instance.TriggerJumpList[0]
                    .transform.GetChild(1)
                    .GetComponent<BoxCollider>()
                    .enabled = false;
            }

            Debug.Log("stars4");
            if (TimeToAnswer.instance.maxTime > 0)
            {
                CollectionController.instance.addStars(10);
            }

            //Destroy(gameObject);
            //pathController.timeToReach.gameObject.SetActive(false);

            /// <summary>
            /// /
            /// </summary>
            /// <returns></returns>
            ///
            /// make buttons no click again
            for (int i = 0; i < 4; i++)
            {
                ansButton[i].onClick.RemoveAllListeners();
            }
            //Time.timeScale = 1;

            /*if (pathController.isTriggerJump)
            {
                //remove the current distance of obstacles
                ObstaclesController.instance.ObstaclesList.RemoveAt(0);
                pathController.isTriggerJump = false;
            }
    */
            //add the questionCount++
            //questionCount++;



            //isQuestionShowUp = false;
            //pathController.isOriginalDistance = false;
            //pathController.timeToReach.gameObject.SetActive(false);
            //destroy and remove the questions from the list
            QuestionController.instance.questionsList.RemoveAt(answers);
            Destroy(GameObject.Find("QuestionsList").transform.GetChild(answers).gameObject);
        }
    }

    IEnumerator incorrectAnswer()
    {
        Debug.Log("Incorrect");
        HideQuestionsButtons();
        //disable the trigger for jumping and code into PathController crash Trigger
        if (pathController.isTriggerJump)
        {
            //colllider jump
            ObstaclesController
                .instance.ObstaclesList[0]
                .transform.GetChild(0)
                .GetComponent<BoxCollider>()
                .enabled = false;

            //collider crash not need because already on
        }

        //this is for the edge pathway jumping
        if (pathController.isTriggerEdge)
        {
            ObstaclesController
                .instance.TriggerJumpList[0]
                .transform.GetChild(0)
                .GetComponent<BoxCollider>()
                .enabled = false;
        }

        /// make buttons no click again
        for (int i = 0; i < 4; i++)
        {
            ansButton[i].onClick.RemoveAllListeners();
        }
        Time.timeScale = 1;
        yield return null;
    }

    IEnumerator incorrectAnswer80()
    {
        speedUpTime();
        Debug.Log("Incorrect");
        HideQuestionsButtons();
        //disable the trigger for jumping and code into PathController crash Trigger
        if (pathController.isTriggerJump)
        {
            //colllider jump
            ObstaclesController
                .instance.ObstaclesList[0]
                .transform.GetChild(0)
                .GetComponent<BoxCollider>()
                .enabled = false;

            //collider crash not need because already on
        }

        //this is for the edge pathway jumping
        if (pathController.isTriggerEdge)
        {
            ObstaclesController
                .instance.TriggerJumpList[0]
                .transform.GetChild(0)
                .GetComponent<BoxCollider>()
                .enabled = false;
        }

        /// make buttons no click again
        for (int i = 0; i < 4; i++)
        {
            ansButton[i].onClick.RemoveAllListeners();
        }
        //Time.timeScale = 1;
        yield return null;
    }

    public void buttonFunction()
    {
        StartCoroutine(incorrectAnswer());
        HideQuestion();
    }

    public void CorrectButtonFunction()
    {
        StartCoroutine(collectedRound());
        HideQuestion();
    }

    public void buttonFunction80()
    {
        StartCoroutine(incorrectAnswer80());
        //HideQuestion80();
    }

    public void CorrectButtonFunction80()
    {
        Debug.Log("correctbuttonfunctioin");
        StartCoroutine(collectedRound80());
        //HideQuestion80();
    }

    public void ShowQuestion()
    {
        if (!isQuestionShowUp)
        {
            Timing.gameObject.SetActive(true);
            operation = rnd2.Next(2);
            //if else level
            if (level >= 1 && level <= 3)
            {
                //questionCount based question

                //LEVEL 1 CHANGING PATHWAY


                if (level == 1)
                {
                    if (questionCount >= 0 && questionCount <= 4)
                    { //answer not more than 10
                    }
                    else if (questionCount > 4 && questionCount <= 7)
                    {
                        // pathController.SetFirstChildToLast();
                        //change character
                        if (enterLevel[1] == false)
                        {
                            //Debug.Log("change characeter once");
                            //pathController.SetFirstChildToLast();
                            enterLevel[0] = false;
                            enterLevel[1] = true;
                        }
                    }
                    else if (questionCount > 7 && questionCount <= 11)
                    {
                        //change character
                        if (enterLevel[2] == false)
                        {
                            //pathController.SetFirstChildToLast();
                            enterLevel[1] = false;
                            enterLevel[2] = true;
                        }
                    }
                    else if (questionCount > 11 && questionCount <= 13)
                    {
                        //change character
                        if (enterLevel[3] == false)
                        {
                            //pathController.SetFirstChildToLast();
                            enterLevel[2] = false;
                            enterLevel[3] = true;
                        }
                    }
                    else if (questionCount > 14)
                    {
                        Debug.Log("more than 14");

                        //change character
                        if (enterLevel[4] == false)
                        {
                            //pathController.SetFirstChildToLast();
                            enterLevel[3] = false;
                            enterLevel[4] = true;
                        }
                    }
                }
                else if (level == 2)
                {
                    if (questionCount >= 0 && questionCount <= 12)
                    { //answer not more than 10
                    }
                    else if (questionCount > 12 && questionCount <= 22)
                    {
                        // pathController.SetFirstChildToLast();
                        //change character
                        if (enterLevel[1] == false)
                        {
                            //Debug.Log("change characeter once");
                            //pathController.SetFirstChildToLast();
                            enterLevel[0] = false;
                            enterLevel[1] = true;
                        }
                    }
                    else if (questionCount > 22 && questionCount <= 33)
                    {
                        //change character
                        if (enterLevel[2] == false)
                        {
                            //pathController.SetFirstChildToLast();
                            enterLevel[1] = false;
                            enterLevel[2] = true;
                        }
                    }
                    else if (questionCount > 33 && questionCount <= 50)
                    {
                        //change character
                        if (enterLevel[3] == false)
                        {
                            //pathController.SetFirstChildToLast();
                            enterLevel[2] = false;
                            enterLevel[3] = true;
                        }
                    }
                    else if (questionCount > 50)
                    {
                        Debug.Log("more than 50");

                        //change character
                        if (enterLevel[4] == false)
                        {
                            //pathController.SetFirstChildToLast();
                            enterLevel[3] = false;
                            enterLevel[4] = true;
                        }
                    }
                }
                else if (level == 3)
                {
                    if (questionCount >= 0 && questionCount <= 8)
                    { //answer not more than 10
                    }
                    else if (questionCount > 8 && questionCount <= 15)
                    {
                        // pathController.SetFirstChildToLast();
                        //change character
                        if (enterLevel[1] == false)
                        {
                            //Debug.Log("change characeter once");
                            //pathController.SetFirstChildToLast();
                            enterLevel[0] = false;
                            enterLevel[1] = true;
                        }
                    }
                    else if (questionCount > 15 && questionCount <= 23)
                    {
                        //change character
                        if (enterLevel[2] == false)
                        {
                            //pathController.SetFirstChildToLast();
                            enterLevel[1] = false;
                            enterLevel[2] = true;
                        }
                    }
                    else if (questionCount > 23 && questionCount <= 35)
                    {
                        //change character
                        if (enterLevel[3] == false)
                        {
                            //pathController.SetFirstChildToLast();
                            enterLevel[2] = false;
                            enterLevel[3] = true;
                        }
                    }
                    else if (questionCount > 35)
                    {
                        Debug.Log("more than 50");

                        //change character
                        if (enterLevel[4] == false)
                        {
                            //pathController.SetFirstChildToLast();
                            enterLevel[3] = false;
                            enterLevel[4] = true;
                        }
                    }
                }
                /*
                if (questionCount >= 0 && questionCount <= 20)
                { //answer not more than 10
                    firstNum = rnd.Next(1, 6); // Generates random integer values between 1 and 5
                    secNum = rnd.Next(1, 6); // Generates random integer values between 1 and 5
                }
                else if (questionCount > 20 && questionCount <= 40)
                {
                    firstNum = rnd.Next(6, 11); // Generates random integer values between 5 and 10
                    secNum = rnd.Next(6, 11); // Generates random integer values between 5 and 10
                    // pathController.SetFirstChildToLast();
                    //change character
                    if (enterLevel[1] == false)
                    {
                        Debug.Log("change characeter once");
                        pathController.SetFirstChildToLast();
                        enterLevel[0] = false;
                        enterLevel[1] = true;
                    }
                }
                else if (questionCount > 40 && questionCount <= 60)
                {
                    firstNum = rnd.Next(11, 16); // Generates random integer values between 10 and 15
                    secNum = rnd.Next(10, 16); // Generates random integer values between 10 and 15
                    //change character
                    if (enterLevel[2] == false)
                    {
                        pathController.SetFirstChildToLast();
                        enterLevel[1] = false;
                        enterLevel[2] = true;
                    }
                }
                else if (questionCount > 60 && questionCount <= 80)
                {
                    firstNum = rnd.Next(16, 21); // Generates random integer values between 10 and 15
                    secNum = rnd.Next(16, 21); // Generates random integer values between 10 and 15
                    //change character
                    if (enterLevel[3] == false)
                    {
                        pathController.SetFirstChildToLast();
                        enterLevel[2] = false;
                        enterLevel[3] = true;
                    }
                }
                else if (questionCount > 80)
                {
                    Debug.Log("more than 80");
                    firstNum = rnd.Next(16, 21); // Generates random integer values between 10 and 15
                    secNum = rnd.Next(16, 21); // Generates random integer values between 10 and 15

                    //change character
                    if (enterLevel[4] == false)
                    {
                        pathController.SetFirstChildToLast();
                        enterLevel[3] = false;
                        enterLevel[4] = true;
                    }
                }
                */
                questionsNum = rnd3.Next(0, QuestionController.instance.questionsList.Count);
                answers = questionsNum;
            }

            //asign to question gameobject
            if (level >= 1)
            {
                questions = "CARI KATA...";
                transform
                    .GetChild(3)
                    .transform.GetChild(0)
                    .transform.GetChild(0)
                    .GetComponent<TextMeshProUGUI>()
                    .text = questions + " ";

                if (
                    !QuestionController
                        .instance.questionsList[answers]
                        .GetComponent<AudioSource>()
                        .isPlaying
                )
                {
                    QuestionController
                        .instance.questionsList[answers]
                        .GetComponent<AudioSource>()
                        .Play();
                }
            }

            //assign buttons in gameobject

            ansButton[0] = transform
                .GetChild(3)
                .transform.GetChild(1)
                .transform.GetChild(0)
                .transform.GetChild(0)
                .GetComponent<Button>();
            ansButton[1] = transform
                .GetChild(3)
                .transform.GetChild(1)
                .transform.GetChild(1)
                .transform.GetChild(0)
                .GetComponent<Button>();
            ansButton[2] = transform
                .GetChild(3)
                .transform.GetChild(2)
                .transform.GetChild(0)
                .transform.GetChild(0)
                .GetComponent<Button>();
            ansButton[3] = transform
                .GetChild(3)
                .transform.GetChild(2)
                .transform.GetChild(1)
                .transform.GetChild(0)
                .GetComponent<Button>();

            //assign random answer to random buttons
            if (level >= 1)
            {
                int length;
                if (level == 1)
                {
                    length = 2;
                    //if level 1 only show two answer buttons
                    ansButton[2].gameObject.SetActive(false);
                    ansButton[3].gameObject.SetActive(false);
                }
                else
                {
                    length = 4;
                }

                //choose which button to put answer
                int buttonNum = rnd.Next(length);
                //add function to button
                ansButton[buttonNum].onClick.AddListener(() => CorrectButtonFunction());

                //add play sound button to soundbtn
                soundBtn.onClick.AddListener(() => PlayCorrectAnswerSound(answers));
                //add something to button child
                ansButton[buttonNum]
                    .transform.GetChild(0)
                    .transform.GetChild(1)
                    .GetComponent<Image>()
                    .sprite = QuestionController
                    .instance.questionsList[answers]
                    .GetComponent<Image>()
                    .sprite;

                if (level == 1)
                {
                    //add image to button child if only level 1
                    ansButton[buttonNum]
                        .transform.GetChild(0)
                        .transform.GetChild(2)
                        .GetComponent<Image>()
                        .sprite = QuestionController
                        .instance.questionsList[answers]
                        .GetChild(0)
                        .GetComponent<Image>()
                        .sprite;
                }

                for (int i = 0; i < length; i++)
                {
                    if (i != buttonNum)
                    {
                        /*
                        if (questionCount >= 0 && questionCount <= 20) // not more than 10
                        {
                            randomAnswer = rnd.Next(2, 11);
                            while (randomAnswer == answers)
                            {
                                randomAnswer = rnd.Next(2, 11);
                            }
                            //                            Debug.Log("answer not more than 10 ");
                        }
                        
                        */

                        //chose random number and check if any same number already in the list
                        int chooseRandom = rnd.Next(
                            0,
                            QuestionController.instance.questionsList.Count
                        );

                        //if list is empty (first time)
                        if (randomAnswer.Count == 0)
                        {
                            while (chooseRandom == questionsNum)
                            {
                                chooseRandom = rnd.Next(
                                    0,
                                    QuestionController.instance.questionsList.Count
                                    );

                            }
                            randomAnswer.Add(chooseRandom);

                        }
                        else
                        {
                            // check if randomnumbers are duplicated in list
                            for (int l = 0; l < randomAnswer.Count; l++)
                            {
                                Debug.Log("Check");
                                while (randomAnswer[l] == chooseRandom)
                                {
                                    Debug.Log("Duplicated");
                                    //generate new number if chooseRandom already in the list
                                    chooseRandom = rnd.Next(
                                        0,
                                        QuestionController.instance.questionsList.Count
                                    );
                                }
                            }
                            //check if answers is already selected from the list
                            while (chooseRandom == answers)
                            {
                                Debug.Log("Duplicated");
                                //generate new number if chooseRandom already in the list
                                chooseRandom = rnd.Next(
                                    0,
                                    QuestionController.instance.questionsList.Count
                                );
                            }

                            //

                            randomAnswer.Add(chooseRandom);
                        }
                        if (level == 1)
                        {
                            //add image to button child if only level 1
                            ansButton[i]
                                .transform.GetChild(0)
                                .transform.GetChild(2)
                                .GetComponent<Image>()
                                .sprite = QuestionController
                                .instance.questionsList[chooseRandom]
                                .GetChild(0)
                                .GetComponent<Image>()
                                .sprite;
                        }
                        ansButton[i].onClick.AddListener(() => buttonFunction());
                        ansButton[i]
                            .transform.GetChild(0)
                            .transform.GetChild(1)
                            .GetComponent<Image>()
                            .sprite = QuestionController
                            .instance.questionsList[chooseRandom]
                            .GetComponent<Image>()
                            .sprite;
                    }
                }
            }

            //play notidication sound
            if (!GameObject.Find("notification").GetComponent<AudioSource>().isPlaying)
            {
                GameObject.Find("notification").GetComponent<AudioSource>().Play();
            }
            //activate background and show question also off center;
            transform.GetChild(0).transform.gameObject.SetActive(true);
            transform.GetChild(2).transform.gameObject.SetActive(false);
            transform.GetChild(3).transform.gameObject.SetActive(true);

            Time.timeScale = 0;
            isQuestionShowUp = true;
        }
    }

    public void PlayCorrectAnswerSound(int answers)
    {
        Debug.Log("play sound");
        if (
            !QuestionController
                .instance.questionsList[answers]
                .GetComponent<AudioSource>()
                .isPlaying
        )
        {
            QuestionController.instance.questionsList[answers].GetComponent<AudioSource>().Play();
        }
    }

    public void ShowQuestion80()
    {
        if (!isQuestionShowUp && QuestionController.instance.questionsList.Count >= 4)
        {
            Timing.gameObject.SetActive(true);
            operation = rnd2.Next(2);
            //if else level
            if (level == 4)
            {
                questionsNum = rnd3.Next(0, QuestionController.instance.questionsList.Count);
                answers = questionsNum;
            }

            //asign to question gameobject
            if (level == 4)
            {
                questions = "CARI KATA.. ";
                transform
                    .GetChild(3)
                    .transform.GetChild(0)
                    .transform.GetChild(0)
                    .GetComponent<TextMeshProUGUI>()
                    .text = questions;

                //bunyi jawapan
                if (
                    !QuestionController
                        .instance.questionsList[answers]
                        .GetComponent<AudioSource>()
                        .isPlaying
                )
                {
                    QuestionController
                        .instance.questionsList[answers]
                        .GetComponent<AudioSource>()
                        .Play();
                }
            }

            //assign buttons in gameobject

            ansButton[0] = transform
                .GetChild(3)
                .transform.GetChild(1)
                .transform.GetChild(0)
                .transform.GetChild(0)
                .GetComponent<Button>();
            ansButton[1] = transform
                .GetChild(3)
                .transform.GetChild(1)
                .transform.GetChild(1)
                .transform.GetChild(0)
                .GetComponent<Button>();
            ansButton[2] = transform
                .GetChild(3)
                .transform.GetChild(2)
                .transform.GetChild(0)
                .transform.GetChild(0)
                .GetComponent<Button>();
            ansButton[3] = transform
                .GetChild(3)
                .transform.GetChild(2)
                .transform.GetChild(1)
                .transform.GetChild(0)
                .GetComponent<Button>();

            //assign random answer to random buttons
            if (level == 4)
            {
                //choose which button to put answer
                int buttonNum = rnd.Next(4);
                ansButton[buttonNum].onClick.AddListener(() => CorrectButtonFunction80());
                ansButton[buttonNum]
                    .transform.GetChild(0)
                    .transform.GetChild(1)
                    .GetComponent<Image>()
                    .sprite = QuestionController
                    .instance.questionsList[answers]
                    .GetComponent<Image>()
                    .sprite;
                //add play sound button to soundbtn
                soundBtn.onClick.AddListener(() => PlayCorrectAnswerSound(answers));
                for (int i = 0; i < 4; i++)
                {
                    if (i != buttonNum)
                    {
                        //chose random number and check if any same number already in the list
                        int chooseRandom = rnd.Next(
                            0,
                            QuestionController.instance.questionsList.Count
                        );

                        //if list is empty (first time)
                        if (randomAnswer.Count == 0)
                        {
                            randomAnswer.Add(chooseRandom);
                        }
                        else
                        {
                            // check if randomnumbers are duplicated in list
                            for (int l = 0; l < randomAnswer.Count; l++)
                            {
                                while (randomAnswer[l] == chooseRandom)
                                {
                                    //generate new number if chooseRandom already in the list
                                    chooseRandom = rnd.Next(
                                        0,
                                        QuestionController.instance.questionsList.Count
                                    );
                                }
                            }
                            //check if answers is already selected from the list
                            while (chooseRandom == answers)
                            { //generate new number if chooseRandom already in the list
                                chooseRandom = rnd.Next(
                                    0,
                                    QuestionController.instance.questionsList.Count
                                );
                            }

                            //

                            randomAnswer.Add(chooseRandom);
                        }

                        ansButton[i].onClick.AddListener(() => buttonFunction80());
                        ansButton[i]
                            .transform.GetChild(0)
                            .transform.GetChild(1)
                            .GetComponent<Image>()
                            .sprite = QuestionController
                            .instance.questionsList[chooseRandom]
                            .GetComponent<Image>()
                            .sprite;
                    }
                }
            }

            //play notidication sound
            if (!GameObject.Find("notification").GetComponent<AudioSource>().isPlaying)
            {
                GameObject.Find("notification").GetComponent<AudioSource>().Play();
            }
            //activate background and show question also off center;
            transform.GetChild(0).transform.gameObject.SetActive(true);
            transform.GetChild(2).transform.gameObject.SetActive(false);
            transform.GetChild(3).transform.gameObject.SetActive(true);

            //Time.timeScale = 0;
            isQuestionShowUp = true;
        }
    }

    public void HideQuestionsButtons()
    {
        //hide background and hide question also off center;
        transform.GetChild(0).transform.gameObject.SetActive(false);
        transform.GetChild(2).transform.gameObject.SetActive(false);
        transform.GetChild(3).transform.gameObject.SetActive(false);
    }

    public void HideQuestion80()
    {
        for (int i = 0; i < 4; i++)
        {
            ansButton[i].onClick.RemoveAllListeners();
        }

        if (pathController.isTriggerJump)
        {
            //remove the current distance of obstacles
            ObstaclesController.instance.ObstaclesList.RemoveAt(0);
            pathController.isTriggerJump = false;
        }

        if (pathController.isTriggerEdge)
        {
            //remove the current distance of obstacles
            ObstaclesController.instance.TriggerJumpList.RemoveAt(0);
            pathController.isTriggerEdge = false;
        } //hide background and hide question also off center;
        transform.GetChild(0).transform.gameObject.SetActive(false);
        transform.GetChild(2).transform.gameObject.SetActive(false);
        transform.GetChild(3).transform.gameObject.SetActive(false);

        //add the questionCount++
        questionCount++;

        Time.timeScale = 1;

        //isQuestionShowUp = false;
        pathController.isOriginalDistance = false;
        pathController.timeToReach.gameObject.SetActive(false);
    }

    public void HideQuestion()
    {
        for (int i = 0; i < 4; i++)
        {
            ansButton[i].onClick.RemoveAllListeners();
        }

        if (pathController.isTriggerJump)
        {
            //remove the current distance of obstacles
            ObstaclesController.instance.ObstaclesList.RemoveAt(0);
            pathController.isTriggerJump = false;
        }

        if (pathController.isTriggerEdge)
        {
            //remove the current distance of obstacles
            ObstaclesController.instance.TriggerJumpList.RemoveAt(0);
            pathController.isTriggerEdge = false;
        } //hide background and hide question also off center;
        transform.GetChild(0).transform.gameObject.SetActive(false);
        transform.GetChild(2).transform.gameObject.SetActive(false);
        transform.GetChild(3).transform.gameObject.SetActive(false);

        //add the questionCount++
        questionCount++;

        Time.timeScale = 1;

        isQuestionShowUp = false;
        pathController.isOriginalDistance = false;
        pathController.timeToReach.gameObject.SetActive(false);

        Timing.gameObject.SetActive(false);
        //remove the random
        randomAnswer.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        soundBtn = transform.GetChild(3).transform.GetChild(3).GetComponent<Button>();
        enterLevel = new bool[6];
        if (level == 4)
        {
            enterLevel[5] = true;
        }
        else
        {
            enterLevel[0] = true;
        }
        Bg = transform.Find("bg").GetComponent<Transform>();
        endGameMenu = transform.Find("EndGameMenu").GetComponent<Transform>();
        endMenuScreen = transform.Find("EndScreen").GetComponent<Transform>();
        moneyTransform = transform.GetChild(1).transform.GetChild(0).GetComponent<Transform>();
        roundTransform = transform.GetChild(1).transform.GetChild(1).GetComponent<Transform>();
        pathController = GameObject.Find("Player").GetComponent<PathController>();
        levelText = transform.GetChild(4).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        questionCount = 0;
        //Time.timeScale = 1;
        gameStart = false;
        SettingBtn = GameObject.Find("SettingButton").transform.GetChild(0).GetComponent<Button>();
        SettingBtn.interactable = false;
    }

    public void freezeTime()
    {
        Time.timeScale = 0f;
    }

    public void unfreezeTime()
    {
        Time.timeScale = 1f;
        //Time.fixedDeltaTime = 1f * .02f;
    }

    public void speedUpTime()
    {
        Time.timeScale = 2f;
        //Time.fixedDeltaTime = 2f * .02f;
    }

    private IEnumerator endThegame()
    {
        yield return new WaitForSeconds(1f);
        gameStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            speedUpTime();
        }
        if (QuestionController.instance.questionsList.Count < 4)
        {
            StartCoroutine(endThegame());
        }
        if (gameStart)
        {
            levelText.text = "LEVEL " + level.ToString();
            if (level >= 1 && level <= 3)
            {
                pathController.timeToReach.gameObject.SetActive(true);
                if (pathController.DistanceBetween < 2f)
                {
                    pathController.timeToReach.gameObject.SetActive(false);
                    pathController.isTriggerJump = true;
                    ShowQuestion();
                }
            }
            else if (level >= 4)
            {
                if (TimeRemainingObtain.instance.timeRemaining <= 2.1f)
                {
                    //     Debug.Log("reset");
                    unfreezeTime();
                }
                if (pathController.DistanceBetween > 0f)
                {
                    //  Debug.Log("level4");
                    StartCoroutine(pathController.displayTimer(1.34f));
                    //pathController.isTriggerJump = true;
                }
            }
        }
        else if (QuestionController.instance.questionsList.Count < 4 && !gameStart)
        {
            gameStart = false;
            //displat end game menu
            endGameMenu.transform.gameObject.SetActive(true);
        }
        //temp disabled
        /*
        if (pathController.DistanceBetweenEdge < 2f)
        {
            pathController.isTriggerEdge = true;
            ShowQuestion();
        }
        */
    }

    public void StartGame()
    {
        StartCoroutine(startingGame());
    }

    IEnumerator startingGame()
    {
        //Time.timeScale = 1;
        CameraAnim.Play("startCameraAnimation");
        yield return new WaitForSeconds(2.3f);

        level = Int32.Parse(SceneManager.GetActiveScene().name);
        gameStart = true;

        FreeLookCamera.gameObject.SetActive(true);
        CameraAnim.enabled = false;
        SettingBtn.interactable = true;
    }
}
