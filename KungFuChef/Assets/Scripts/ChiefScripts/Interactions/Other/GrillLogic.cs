using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrillLogic : MonoBehaviour
{
    public bool turnedOn;

	// Use this for initialization
	void Start ()
    {
        turnedOn = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		//if(!turnedOn)
        {
            //gameObject.SetActive(false);
        }
	}

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Food" || col.tag == "RawFood")
        {
            col.GetComponent<StartGrill>().enabled = true;
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.tag == "Food" || col.tag == "RawFood")
        {
            col.GetComponent<StartGrill>().enabled = false;
        }
    }
}
