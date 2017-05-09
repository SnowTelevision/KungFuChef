using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightScript : MonoBehaviour
{
    public Coroutine lightCo;
    public Light lightBar;

    public float time; 
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(lightCo == null)
        {
            StartCoroutine(Flicker());
        }
	}

    public IEnumerator Flicker()
    {
        while (true)
        {
            lightBar.enabled = false;
            yield return new WaitForSeconds(time);
            lightBar.enabled = true;
            yield return new WaitForSeconds(time);
        }
    }

}
