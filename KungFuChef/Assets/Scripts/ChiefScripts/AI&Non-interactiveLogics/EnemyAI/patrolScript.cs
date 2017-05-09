using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolScript : MonoBehaviour {

    public Rigidbody rigidBody;
    public float turnTime;
    public Vector3 rotation;
    public Vector3 normalRotation;

    public GameObject randomizedTarget;                       //the temporary target in the patrol area, that randomizes the movement
    public GameObject patrolArea;                   //the patrolling area for this drone

    public float speed;                             //the speed of the drone movement                
    private bool isThereRandom;                     //to check if the temporary target has been assigned another position in the patrol area

    private float tempX, tempY;                     //coordinates of the temporary target

    void Start () {
        isThereRandom = false;                                  //currently, the drone is not assigned a random position in the patrolling area
        transform.rotation = patrolArea.transform.rotation;     //aligning the drone with the patrol area
        transform.eulerAngles = new Vector3(0, 180, 0);         //adjustments in the drone alignment, as the patrol area sphere faces opposite
        StartCoroutine(turn());
    }

    void Update()
    {
        if (isThereRandom == false)                             //checking if new random position should be assigned or not
        {
            tempX = Random.Range(-0.5f, 0.5f);                  //calculating a temporary position in the circle area
            tempY = Mathf.Sqrt(0.25f - Mathf.Pow(tempX, 2)); 
            tempY = Random.Range(-tempY, tempY);

            randomizedTarget.transform.localPosition = new Vector3(tempX, tempY, 0);  //position relative to the patrolArea
            isThereRandom = true;                                           //randomized target is assigned, now the drone can move there
        }
        else
        {
            //transform.position = Vector3.MoveTowards(transform.position, randomizedTarget.transform.position, speed); //drone moving towards the randomized target
            rigidBody.velocity = transform.forward.normalized * speed;
            if ((transform.position - randomizedTarget.transform.position).magnitude <= 0.1f)
            {
                isThereRandom = false;                  //after reaching the randomized target, a new target will be chosen
            }
        }
    }

    void OnCollisionEnter(Collision other)              //On collision with the plasma beam, the drone will be destroyed
    {
        //print(other.collider.name);
        //if(ReferenceEquals(other.gameObject, randomizedTarget))
        //{
        //    isThereRandom = false;
        //}

        if (other.gameObject.CompareTag("plasma"))
        {
            patrolArea.GetComponent<patrolDroneSpawner>().currentCountOfDrones--;   //reducing the count in the spawner script
            Destroy(randomizedTarget);
            Destroy(this.gameObject);
        }
    }

    //void OnMouseDown()
    //{
    //    patrolArea.GetComponent<patrolDroneSpawner>().currentCountOfDrones--;
    //    Destroy(this.gameObject);
    //}
    

    IEnumerator turn()
    {
    //    float oldX;
    //    float oldY;
    //    float oldZ;
        //float newX;
        //float newY;
        //float newZ;

        while (true)
        {
        //    oldX = transform.eulerAngles.x;
        //    oldY = transform.eulerAngles.y;
        //    oldZ = transform.eulerAngles.z;
            transform.LookAt(randomizedTarget.transform);
            //newX = transform.eulerAngles.x;
            //newY = transform.eulerAngles.y;
            //newZ = transform.eulerAngles.z;

            //for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / turnTime)
            //{
            //    rotation.x = Mathf.LerpAngle(oldX, newX, t);
            //    rotation.y = Mathf.LerpAngle(oldY, newY, t);
            //    rotation.z = Mathf.LerpAngle(oldZ, newZ, t);

            //    transform.eulerAngles = rotation;
            //    //rigidBody.velocity = transform.forward.normalized * speed;
            //    //transform.eulerAngles = normalRotation;
            //    yield return null;
            //}
            yield return null;
        }
    }
}

