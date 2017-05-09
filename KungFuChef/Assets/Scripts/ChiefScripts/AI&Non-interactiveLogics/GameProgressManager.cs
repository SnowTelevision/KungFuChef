using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressManager : MonoBehaviour
{
    public DisplayOrder orderDisplay;
    public VerifyOrder orderVerifier;
    public float orderTimeLimit;
    public attackDroneSpawner atkSpawner;
    public patrolDroneSpawner defSpawner;
    public float scoreToDef;
    public float scoreToAtk;
    public GameObject portalDoor;
    public float initialAtkDelay;
    public float initialDefDelay;

    public float currentOrderStartTime;
    public float moneyScore;
    public float defStartTime;
    public float atkStartTime;
    public float minDroneSpawnInterval;

	// Use this for initialization
	void Start ()
    {
        orderDisplay.displayOrderFunc();
        currentOrderStartTime = Time.time;
        atkSpawner.delayBetweenSpawningDrones = initialAtkDelay;
        defSpawner.delayBetweenSpawningDrones = initialDefDelay;
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

        if(moneyScore >= 200 && !defSpawner.enabled)
        {
            defSpawner.enabled = true;
            defStartTime = Time.time;
        }

        if (moneyScore >= 500 && !atkSpawner.enabled)
        {
            atkSpawner.enabled = true;
            defStartTime = Time.time;
        }

        if (atkSpawner.delayBetweenSpawningDrones >= minDroneSpawnInterval)
        {
            atkSpawner.delayBetweenSpawningDrones = initialDefDelay - ((Time.time - atkStartTime) / 5f);
        }

        if (atkSpawner.delayBetweenSpawningDrones >= minDroneSpawnInterval)
        {
            atkSpawner.delayBetweenSpawningDrones = initialDefDelay - ((Time.time - atkStartTime) / 5f);
        }
    }

    public void gameOverLogic()
    {

    }
}
