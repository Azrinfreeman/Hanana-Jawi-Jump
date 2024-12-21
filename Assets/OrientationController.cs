using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationController : MonoBehaviour
{
    public bool isLandscape;
    // Start is called before the first frame update
    void Start()
    {
        if(!isLandscape){

        Screen.orientation = ScreenOrientation.Portrait;
        }
        else{
            
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
