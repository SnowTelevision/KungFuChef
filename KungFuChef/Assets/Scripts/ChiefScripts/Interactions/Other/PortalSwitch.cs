using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PortalSwitch : VRTK_InteractableObject
{
    public GameObject portalDoor;

    public bool isPortalOpen;
    public MeshRenderer meshColor;

    // Use this for initialization
    void Start ()
    {
        meshColor = GetComponent<MeshRenderer>();
    }
	
    public override void StartUsing(GameObject usingObject)
    {
        if (portalDoor.activeSelf)
        {
            portalDoor.SetActive(false);
            meshColor.material.color = Color.yellow;
        }

        else if (!portalDoor.activeSelf)
        {
            portalDoor.SetActive(true);
            meshColor.material.color = Color.white;
        }
    }

    //public void OnTriggerEnter(Collider col)
    //{
    //    if (col.tag == "ViveController")
    //    {
    //        if (portalDoor.activeSelf)
    //        {
    //            portalDoor.SetActive(false);
    //            meshColor.material.color = Color.yellow;
    //        }

    //        else if (!portalDoor.activeSelf)
    //        {
    //            portalDoor.SetActive(true);
    //            meshColor.material.color = Color.white;
    //        }
    //    }
    //}
}
