using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSwitch : MonoBehaviour
{
    public GameObject portalDoor;

    public bool isPortalOpen;
    public MeshRenderer meshColor;

    // Use this for initialization
    void Start ()
    {
        meshColor = GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "ViveController")
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
    }
}
