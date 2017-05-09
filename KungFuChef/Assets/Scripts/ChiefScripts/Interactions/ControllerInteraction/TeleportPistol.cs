using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TeleportPistol : VRTK_InteractableObject
{
    public GameObject pistol;
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
        pistol.GetComponent<Rigidbody>().velocity = Vector3.zero;
        pistol.transform.position = teleportPosi.position;
        pistol.transform.rotation = teleportPosi.rotation;
    }
}
