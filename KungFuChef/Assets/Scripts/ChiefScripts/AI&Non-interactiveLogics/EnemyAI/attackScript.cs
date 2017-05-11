using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackScript : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float turnTime;
    public Vector3 rotation;
    public float minZoff;
    public float maxZoff;
    public GameObject zapChargeAni;
    public GameProgressManager gameManager;
    public GameObject lights;

    public float distBetween;                   //variable to store the distance between the player and the drone

    public GameObject followee;                 //assigning the player as the one to follow and zap
    public GameObject patrolArea;               //assigning the initial patrol area of this drone
    public GameObject initialPosition;          //assigning the initial position on the patrol area
    public float speed;                         //speed of the drone
    public float waitingTime;                   //time to wait at the initial position before pursuing the player
    public float zapWaitingTime;                //time to wait after reaching the player and zapping
    public float zapDistance;                   //distance to zap the player
    public float startChargeDistance;           //distance from the player from where the drone will wait and zap

    private Coroutine currentCoroutine = null;  //variable to store the coroutine, this is need to stop the coroutine when desired

    public int state;                          // 0 = going towards patrol area, 1 = wait at initial position,
                                                // 2 = going towards player till reaching the zap distance, 3 = stopping the wait coroutine if the player moves away from the drone,
                                                // 4 = waiting before the zap, also checking if the player has moved away more than the zap distance

    private float tempX, tempY;                 //coordinates of the temporary target

    void Start()
    {
        state = 0;                                          //putting the initial state

        tempX = Random.Range(-0.5f, 0.5f);                  //calculating a temporary position in the circle area
        tempY = Mathf.Sqrt(0.25f - Mathf.Pow(tempX, 2));
        tempY = Random.Range(-tempY, tempY);
        initialPosition.transform.localPosition = new Vector3(tempX, tempY, Random.Range(minZoff, maxZoff));  //assigning a temporary position in the patrol area
        //print(initialPosition.transform.localPosition);
        //this position is relative to the patrol area because initialPosition is a child of patrolArea
        StartCoroutine(turn());

        gameManager = FindObjectOfType<GameProgressManager>();
    }

    void FixedUpdate()                                       
    {
        //rotateUnit(followee.transform.position);            //making the drone face the player all the time

        if (gameManager.portalDoor.activeInHierarchy && lights.activeInHierarchy) //if the door is close then turn off drone lights
        {
            lights.SetActive(false);
        }
        if (!gameManager.portalDoor.activeInHierarchy && !lights.activeInHierarchy) //if the door is open then turn on drone lights
        {
            lights.SetActive(true);
        }

        if (state != 0)
        {
            distBetween = (transform.position - followee.transform.position).magnitude;
            if (distBetween > zapDistance)
            {
                //transform.position = Vector3.MoveTowards(transform.position, followee.transform.position, speed);
                rigidBody.velocity = transform.forward.normalized * speed;
            }
        }

        switch (state)                          
        {
            case 0:                                         
                transform.position = Vector3.MoveTowards(transform.position, initialPosition.transform.position, speed / 40f);    //moving towards the patrol area
                if (transform.position == initialPosition.transform.position)           //checking if reached the initial position
                {
                    state = 1;                                            //starting wait before pursuing the player   
                    Destroy(initialPosition.gameObject);           
                    StartCoroutine(normalWait());
                }
                break;

            case 1:                                                     //waiting for the coroutine to pass control to pursue
                break;

            case 2:                                                     //pursuing the player
                if(distBetween < startChargeDistance)
                {
                    currentCoroutine = StartCoroutine(zapWait());       //starting to wait before zapping
                    state = 4;
                }
                break;

            case 3:
                state = 2;
                if (currentCoroutine != null)                            //stopping the zap wait and start to pursue again
                {
                    StopCoroutine(currentCoroutine);
                    zapChargeAni.SetActive(false);
                }
                break;

            case 4:
                distBetween = (transform.position - followee.transform.position).magnitude;
                if (distBetween > startChargeDistance)                          //checking if the player is going away while waiting to zap
                    state = 3;                                          //if the player is moving away, cancelling the zap wait and pursue again
                break;

            default: break;
        }

    }

    void rotateUnit(Vector3 target)                                     //function to rotate towards the player
    {
        Vector3 relativePos = target - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;
    }

    IEnumerator normalWait()                                            //code to wait at the initial position on the patrol area
    {
        yield return new WaitForSeconds(waitingTime);
        state = 2;
    }

    IEnumerator zapWait()                                               //code to wait for zapping
    {
        zapChargeAni.SetActive(true);
        yield return new WaitForSeconds(zapWaitingTime);
        //Zap logic down below

        if((transform.position - followee.transform.position).magnitude <= zapDistance)
        {
            gameManager.gameOverLogic();
        }
    }

    void OnCollisionEnter(Collision other)                              //detecting being shot by the plasma gun
    {
        if (other.gameObject.CompareTag("plasma") || other.transform.name == "BladeEdgeA" || other.transform.name == "BladeEdgeB" || other.transform.name == "BladeEdge")
        {
            patrolArea.GetComponent<attackDroneSpawner>().currentCountOfDrones--;   //decrmenting the current count of the drones in spawner script
            Destroy(this.gameObject);                                               //destroying this drone
        }
    }

    //void OnMouseDown()
    //{
    //    print("aaa");
    //    Destroy(initialPosition);
    //    patrolArea.GetComponent<attackDroneSpawner>().currentCountOfDrones--;
    //    Destroy(this.gameObject);
    //}

    IEnumerator turn()
    {
        //float oldX;
        //float oldY;
        //float oldZ;
        //float newX;
        //float newY;
        //float newZ;

        while (true)
        {
            //oldX = transform.eulerAngles.x;
            //oldY = transform.eulerAngles.y;
            //oldZ = transform.eulerAngles.z;
            transform.LookAt(followee.transform);
            //newX = transform.eulerAngles.x;
            //newY = transform.eulerAngles.y;
            //newZ = transform.eulerAngles.z;

            //for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / turnTime)
            //{
            //    rotation.x = Mathf.LerpAngle(oldX, newX, t);
            //    rotation.y = Mathf.LerpAngle(oldY, newY, t);
            //    rotation.z = Mathf.LerpAngle(oldZ, newZ, t);

            //    transform.eulerAngles = rotation;
            //    yield return null;
            //}
            yield return null;
        }
    }
}
