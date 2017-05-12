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
    public Text scoreText;

    public float currentOrderStartTime;
    public float moneyScore;
    public float minDefDroneSpawnInterval;
    public float minAtkDroneSpawnInterval;
    public bool startAtk;
    public bool startDef;
    public float timeAfterLastDef;
    public float timeAfterLastAtk;
    public float lastDefSpawnTime;
    public float lastAtkSpawnTime;
    public float pauseStartTime;
    public float totalPausingTimeDef;
    public float totalPausingTimeAtk;
    public bool isSpawnerPausing;
    public float defStartTime;
    public float atkStartTime;

    // Use this for initialization
    void Start()
    {
        orderDisplay.displayOrderFunc();
        currentOrderStartTime = Time.time;
        atkSpawner.delayBetweenSpawningDrones = initialAtkDelay;
        defSpawner.delayBetweenSpawningDrones = initialDefDelay;
        moneyScore = 250;
        startAtk = false;
        startDef = false;
        lastDefSpawnTime = 0;
        lastAtkSpawnTime = 0;



        pauseStartTime = 0;
        totalPausingTimeDef = 0;
        totalPausingTimeAtk = 0;
        isSpawnerPausing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - currentOrderStartTime >= orderTimeLimit)
        {
            orderVerifier.scoreText.text = "Time Out!";
            orderDisplay.displayOrderFunc();
            currentOrderStartTime = Time.time;
        }

        if (!portalDoor.activeInHierarchy) //when portal is opening
        {
            atkSpawner.canSpawnDrone = true;
            defSpawner.canSpawnDrone = true;

            if (isSpawnerPausing)
            {
                isSpawnerPausing = false;
            }

            if (startDef && defSpawner.currentCountOfDrones < defSpawner.maxNumberOfDrones)
            {
                timeAfterLastDef += Time.deltaTime; //counting how much time past since last drone spawn

                if (defSpawner.delayBetweenSpawningDrones >= minDefDroneSpawnInterval)
                {
                    defSpawner.delayBetweenSpawningDrones = initialDefDelay - (Time.deltaTime / 5f); //shorten def drone spawn delay as time goes
                }
            }

            if (startAtk && atkSpawner.currentCountOfDrones < defSpawner.maxNumberOfDrones)
            {
                timeAfterLastAtk += Time.deltaTime;

                if (atkSpawner.delayBetweenSpawningDrones >= minAtkDroneSpawnInterval)
                {
                    atkSpawner.delayBetweenSpawningDrones = initialAtkDelay - (Time.deltaTime / 5f); //shorten atk drone spawn delay as time goes
                }
            }

        }

        if (portalDoor.activeInHierarchy) //when portal is closing
        {
            atkSpawner.canSpawnDrone = false;
            defSpawner.canSpawnDrone = false;

            if (!isSpawnerPausing)
            {
                isSpawnerPausing = true;
            }
        }

        if (moneyScore >= 200 && !startDef && !isSpawnerPausing)
        {
            startDef = true;
            defSpawner.spawnDrone();
        }

        if (moneyScore >= 500 && !startAtk && !isSpawnerPausing)
        {
            startAtk = true;
            atkSpawner.spawnDrone();
        }

        //if (defSpawner.delayBetweenSpawningDrones >= minDefDroneSpawnInterval && startDef && !isSpawnerPausing) //shorten def drone spawn delay as time goes
        //{
        //    defSpawner.delayBetweenSpawningDrones = initialDefDelay - ((Time.time - defStartTime) / 5f);
        //}

        //if (atkSpawner.delayBetweenSpawningDrones >= minAtkDroneSpawnInterval && startAtk && !isSpawnerPausing) //shorten atk drone spawn delay as time goes
        //{
        //    atkSpawner.delayBetweenSpawningDrones = initialAtkDelay - ((Time.time - atkStartTime) / 5f);
        //}

        //print(defSpawner.canSpawnDrone);
        //print(timeAfterLastDef - defSpawner.delayBetweenSpawningDrones);
        //print(defSpawner.currentCountOfDrones - defSpawner.maxNumberOfDrones);
        //print(startDef);

        if (defSpawner.canSpawnDrone && timeAfterLastDef >= defSpawner.delayBetweenSpawningDrones && defSpawner.currentCountOfDrones < defSpawner.maxNumberOfDrones && startDef) //spawn def drone
        {
            defSpawner.spawnDrone();
            timeAfterLastDef = 0;
        }

        if (atkSpawner.canSpawnDrone && timeAfterLastAtk >= atkSpawner.delayBetweenSpawningDrones && atkSpawner.currentCountOfDrones < defSpawner.maxNumberOfDrones && startAtk) //spawn atk drone
        {
            atkSpawner.spawnDrone();
            timeAfterLastAtk = 0;
        }

        scoreText.text = "$" + moneyScore;
    }

    public void gameOverLogic()
    {

    }
}
