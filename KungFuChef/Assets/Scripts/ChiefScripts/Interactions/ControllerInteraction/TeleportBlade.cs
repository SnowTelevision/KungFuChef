using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TeleportBlade : VRTK_InteractableObject
{
    public GameObject blade;
    public Transform teleportPosi;

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
        blade.GetComponent<Rigidbody>().velocity = Vector3.zero;
        blade.transform.position = teleportPosi.position;
        blade.transform.rotation = teleportPosi.rotation;
    }
}
