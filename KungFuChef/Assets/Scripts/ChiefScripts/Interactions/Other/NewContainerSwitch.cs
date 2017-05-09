using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class NewContainerSwitch : VRTK_InteractableObject
{
    public GeneratePlate containerGenerator;

	// Use this for initialization
	void Start ()
    {
		
	}

    public override void StartUsing(GameObject usingObject)
    {
        containerGenerator.createTarget();
    }

    //public void OnTriggerEnter(Collider col)
    //{
    //    if (col.tag == "ViveController")
    //    {
    //        containerGenerator.createTarget();
    //    }
    //}
}
