using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrillSwitch : MonoBehaviour
{
    public GameObject grillTrigger;
    public GrillLogic triggerLogic;
    public Vector3 onPosition;
    public Vector3 offPosition;

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
            if (!grillTrigger.activeInHierarchy)
            {
                grillTrigger.transform.localPosition = onPosition;
                meshColor.material.color = Color.yellow;
                StartCoroutine(grillOnOff());
            }

            else if (grillTrigger.activeInHierarchy)
            {
                grillTrigger.transform.localPosition = offPosition;
                meshColor.material.color = Color.white;
                StartCoroutine(grillOnOff());
            }
        }
    }

    IEnumerator grillOnOff()
    {
        yield return new WaitForSeconds(0.1f);

        if(triggerLogic.turnedOn)
        {
            triggerLogic.turnedOn = false;
            grillTrigger.SetActive(false);
        }

        else if(!triggerLogic.turnedOn)
        {
            grillTrigger.SetActive(true);
            triggerLogic.turnedOn = true;
        }
    }
}
