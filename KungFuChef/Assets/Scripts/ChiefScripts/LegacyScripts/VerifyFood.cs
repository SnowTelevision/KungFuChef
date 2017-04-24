using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifyFood : MonoBehaviour
{


    public int tomatoCount;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider col)
    {
        //print(col.name + " entered");

        if(col.name == "TomatoSliced")
        {
            tomatoCount++;
        }
    }

    void OnTriggerExit(Collider col)
    {
        //print(col.name + " exited");

        if (col.name == "TomatoSliced")
        {
            tomatoCount--;
        }
    }
}
