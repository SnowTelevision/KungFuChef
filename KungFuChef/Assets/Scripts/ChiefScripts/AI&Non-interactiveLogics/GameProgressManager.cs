using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressManager : MonoBehaviour
{
    public DisplayOrder orderDisplay;
    public VerifyOrder orderVerifier;
    public float orderTimeLimit;

    public float currentOrderStartTime;

	// Use this for initialization
	void Start ()
    {
        orderDisplay.displayOrderFunc();
        currentOrderStartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Time.time - currentOrderStartTime >= orderTimeLimit)
        {
            orderVerifier.scoreText.text = "Time Out!";
            orderDisplay.displayOrderFunc();
            currentOrderStartTime = Time.time;
        }
	}
}
