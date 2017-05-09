using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ShootPistol : VRTK_InteractableObject
{
    public MultiShooter trigger;

	// Use this for initialization
	void Start ()
    {
		
	}

    // Update is called once per frame
    //void Update ()
    //   {

    //}

    public override void StartUsing(GameObject usingObject)
    {
        trigger.shoot();


    }
}
