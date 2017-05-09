using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackDroneSpawner : MonoBehaviour {
    private int totalSpawnedUptilNow;

    public float delayBetweenSpawningDrones;                      //delay to spawn a new drone
    public int maxNumberOfDrones;                               //to decide the number of drones

    public GameObject initialPositionOnPatrolArea;              //assign the Target prefab to this Game Object in the Inspector
    public GameObject drone;                                    //assign the attackDrone prefab to this Game Object in the Inspector
    public float spawnRadius;                                   //considering radius of the patrol area as one, relative spawn radius
    public GameObject player;                                   //assign the Player object from the scene to this Game Object in the Inspector

    public float speedOfAttackDrone;                            //speed of the attack drone
    public float waitBeforeInitialPursue;                //waiting time before pursuing the player
    public float waitBeforeZapping;                      //waiting time before zapping the player
    public float zappingDistance;                               //distance from the where to zap the player
    [HideInInspector] public int currentCountOfDrones;          //current count of attack drones in the scene

    private float tempX, tempY;                                 //variables for handling random spawn location of the attack drones

    private GameObject tempDrone;                               //temporary instance of drone, just of data handling
    private GameObject tempTarget;                              //temporary instance of randomized target, just for data handling

    private bool canSpawnDrone;                                 //variable to check if we have waited enough to spawn the next drone

	void Start () {
        totalSpawnedUptilNow = 0;                               
        canSpawnDrone = true;
        currentCountOfDrones = 0;
        StartCoroutine(allowDroneSpawning());

    }
	
	void Update () {
		
        //if(currentCountOfDrones < maxNumberOfDrones && canSpawnDrone==true) //checking if maximum number of drones is reached
        //{
        //    canSpawnDrone = false;                                      
        //    StartCoroutine(allowDroneSpawning());                           //starting the wait time before spawning the drone
        //}

        //if (canSpawnDrone == true) //checking if maximum number of drones is reached
        //{
        //    canSpawnDrone = false;
        //    StartCoroutine(allowDroneSpawning());                           //starting the wait time before spawning the drone
        //}
    }
    
    IEnumerator allowDroneSpawning()                                        
    {
        while (canSpawnDrone)
        {
            canSpawnDrone = true;                                               //this is true means we have done the waiting
            spawnDrone();
            yield return new WaitForSeconds(delayBetweenSpawningDrones);        //waiting before spawning the drone
        }
    }

    void spawnDrone()
    {
        tempX = Random.Range(-spawnRadius, spawnRadius);                        //finding a random x position for the potential spawn point
        tempY = Mathf.Sqrt(Mathf.Pow(spawnRadius, 2) - Mathf.Pow(tempX, 2));    //using the equation of a circle to find the y position
        if (Random.Range(-1, 1) < 0)                                            //choosing either a negative or a positive value for y position
            tempY = -tempY;
        Vector3 globalPosition = transform.TransformPoint(new Vector3(tempX, tempY, 0));    //converting the relative position to the patrol area into world position

        tempDrone = Instantiate(drone, globalPosition, Quaternion.identity);        //instantiating a new drone

        tempTarget = Instantiate(initialPositionOnPatrolArea, transform.position, Quaternion.identity);  //instantiating an initial position on the patrol area to pass to the drone
        tempTarget.transform.parent = this.gameObject.transform;                     //making this initial position a child to the patrol area
        tempDrone.GetComponent<attackScript>().initialPosition = tempTarget;         //assigning the initial position to the new drone

        tempDrone.GetComponent<attackScript>().followee = player;                   //assigning the player to the drone as a zapping target
        tempDrone.GetComponent<attackScript>().patrolArea = this.gameObject;        //assigning a patrol area to the new drone
        tempDrone.GetComponent<attackScript>().speed = speedOfAttackDrone;                //assigning a speed to the new drone
        tempDrone.GetComponent<attackScript>().waitingTime = waitBeforeInitialPursue;       //assigning a waiting time to the attack drone
        tempDrone.GetComponent<attackScript>().zapWaitingTime = waitBeforeZapping;          //assigning a waiting time before zapping
        tempDrone.GetComponent<attackScript>().zapDistance = zappingDistance;                      //assigning a zap distance

        currentCountOfDrones++;                                                     //incrementing the count of the current drones
        totalSpawnedUptilNow++;                                                     //calculating total drones spawned altogether
        //print("total attack drones:  "+totalSpawnedUptilNow);                       //displaying the total number of attack drones
    }
    

}
