﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewContainerSwitch : MonoBehaviour
{
    public GeneratePlate containerGenerator;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "ViveController")
        {
            containerGenerator.createTarget();
        }
    }
}