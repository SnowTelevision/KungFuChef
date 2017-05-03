using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderTimeRemainingBar : MonoBehaviour
{
    public Image bar;
    public GameProgressManager gameManager;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        bar.fillAmount = (gameManager.currentOrderStartTime + gameManager.orderTimeLimit - Time.time) / gameManager.orderTimeLimit;
	}
}
