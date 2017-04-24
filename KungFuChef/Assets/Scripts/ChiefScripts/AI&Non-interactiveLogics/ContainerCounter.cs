using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : MonoBehaviour
{
    public int containerCount;
    public GeneratePlate plateGenerator;

	// Use this for initialization
	void Start ()
    {
        containerCount = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {

    }
    
    void OnTriggerEnter(Collider col)
    {
        //print(col.name + " entered");

        if (col.tag == "FoodContainer")
        {
            containerCount++;
        }
    }

    void OnTriggerExit(Collider col)
    {
        //print(col.name + " exited");

        if (col.name == "FoodContainer")
        {
            containerCount--;

            if(containerCount == 0)
            {
                plateGenerator.createTarget();
            }
        }
    }
}
