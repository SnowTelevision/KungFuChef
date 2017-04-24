using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookStatus : MonoBehaviour
{
    public float minCookTime; //The minimum time required to finish cooking
    public float maxCookTime; //The maximum time allowed for cooking before it burnt
    public float grillSpeed; //how much percent the color change from original to target in 1 second
    public int foodType;

    public float cookedTime;
    public bool isGood; //If the food is in good status (is it finished cooking/processing)

	// Use this for initialization
	void Start ()
    {
        cookedTime = 0;
        isGood = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(cookedTime >= minCookTime && cookedTime <= maxCookTime)
        {
            if(!isGood)
            {
                //Play finish cooking animation
            }

            isGood = true;
        }

        else if (cookedTime > maxCookTime)
        {
            if(isGood)
            {
                //Play over cook animation
            }
            isGood = false;
        }
	}
}
