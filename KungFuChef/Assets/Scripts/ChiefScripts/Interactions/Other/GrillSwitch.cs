using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrillSwitch : MonoBehaviour
{
    public GameObject grillTrigger;
    public GrillLogic triggerLogic;
    public Vector3 onPosition;
    public Vector3 offPosition;

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
            if (!grillTrigger.activeInHierarchy)
            {
                grillTrigger.transform.localPosition = onPosition;
                StartCoroutine(grillOnOff());
            }

            else if (grillTrigger.activeInHierarchy)
            {
                grillTrigger.transform.localPosition = offPosition;
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
            triggerLogic.turnedOn = true;
            grillTrigger.SetActive(true);
        }
    }
}
