using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRemainingObtain : MonoBehaviour
{
    public static TimeRemainingObtain instance;

    void Awake()
    {
        instance = this;
    }

    public PathController pathController;
    public float timeRemaining;
    private float timeFull;

    // Start is called before the first frame update
    void Start()
    {
        pathController = GameObject.Find("Player").GetComponent<PathController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.gameStart)
        {
            timeFull = pathController.OriginalDistanceBetween / pathController.moveSpeed;

            timeRemaining = pathController.DistanceBetween / pathController.moveSpeed;
        }
    }
}
