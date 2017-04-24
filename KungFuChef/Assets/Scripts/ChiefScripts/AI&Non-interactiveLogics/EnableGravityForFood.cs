using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGravityForFood : MonoBehaviour
{

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

        if (col.tag == "Food")
        {
            col.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
