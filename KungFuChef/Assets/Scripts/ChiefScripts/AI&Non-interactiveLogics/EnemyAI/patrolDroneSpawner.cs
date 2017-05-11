using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolDroneSpawner : MonoBehaviour {
    //the user can decide the delay for spawning, the number of drones in a region
    //Although, to set the values of the drone, the player needs to do that in the script

    private int totalSpawnedUptilNow;                       //total number of patrol drones spawned uptil now

    public float delayBetweenSpawningDrones;                  //delay to spawn a new drone
    public int maxNumberOfDrones;                           //to decide the number of drones
    public float speedOfPatrolDrone;                        //to assign the speed of the drone
    public float spawnRadius;                               //considering radius of the patrol area as 1, relative spawn radius

    public GameObject drone;                                //assign the patrolDrone prefab in the inspector here
    public GameObject randomizedTargetOnPatrolArea;         //assign the Target prefab in the inspector here
                                                            //This is the dynamically changing position on the patrol area

    private float tempX, tempY;                             //variables to store random position values
    public int currentCountOfDrones;      //variable to keep a track on the current count of patrol drones

    private GameObject tempDrone;                           //temporary instance of drone, just of data handling
    private GameObject tempTarget;                          //temporary instance of randomized target, just for data handling
    public bool canSpawnDrone;                             //boolean to check if we have waited before spawning the next drone   

	void Start () {
        totalSpawnedUptilNow = 0;   
        currentCountOfDrones = 0;
        canSpawnDrone = true;
        //StartCoroutine(allowDroneSpawning());
    }
	
	void Update ()
    {
        //if(currentCountOfDrones < maxNumberOfDrones && canSpawnDrone==true) //checking if max number of drones has been reached
        //{
        //    canSpawnDrone = false;
        //    StartCoroutine(allowDroneSpawning());                       //starting a coroutine to wait before spawning
        //}
	}
    
    IEnumerator allowDroneSpawning()
    {
        while (true)
        {
            if (canSpawnDrone)
            {
                if (currentCountOfDrones < maxNumberOfDrones)
                {
                    spawnDrone();                                                       //function to spawn the drone
                }
            }

            yield return new WaitForSeconds(delayBetweenSpawningDrones);        //code to wait before spawning
        }
    }

    public void spawnDrone()
    {
        tempX = Random.Range(-spawnRadius, spawnRadius);                        //finding a random x position for the potential spawn point
        tempY = Mathf.Sqrt(Mathf.Pow(spawnRadius, 2) - Mathf.Pow(tempX, 2));    //using the circle equation, finding the equivalent y position
        if (Random.Range(-1, 1) < 0)                                            //giving either a positive or a negative value to y position
            tempY = -tempY;
        Vector3 globalPosition = transform.TransformPoint(new Vector3(tempX, tempY, 0));
        //The x and y coordinates obtained are relative to the position and scale of the patrol area. The above line converts it into a world position

        tempDrone = Instantiate(drone, globalPosition, Quaternion.identity);             //instantiating a new drone
        tempTarget = Instantiate(randomizedTargetOnPatrolArea, transform.position, Quaternion.identity); //instantiating a new randomizing target
        tempTarget.transform.parent = this.gameObject.transform;                                        
        //Making the randomizing target a child of the patrol area, so that it is used properly in the script of the drone

        tempDrone.GetComponent<patrolScript>().randomizedTarget = tempTarget;             //assigning a randomizing target to the new drone
        tempDrone.GetComponent<patrolScript>().patrolArea = this.gameObject;    //assigning a patrol area to the new drone
        tempDrone.GetComponent<patrolScript>().speed = speedOfPatrolDrone;      //assigning a speed to the new drone

        currentCountOfDrones++;                                                 //adding the count of the current drones
        totalSpawnedUptilNow++;                                                 //calculating total drones spawned altogether
        //print("total patrol drones: "+totalSpawnedUptilNow);                    //printing the total

    }


}
