using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGrill : MonoBehaviour
{
    public GameObject[] contents;
    public Color targetGrillColor;
    public Color originalColor;
    public CookStatus cookStatus;

    public float grillSpeed; // how much percent the color change from original to target in 1 second
    public Color newColor;

	// Use this for initialization
	void Start ()
    {
        grillSpeed = cookStatus.grillSpeed;
	}
	
	// Update is called once per frame
	void Update ()
    {
		for(int i = 0; i < contents.Length; i++)
        {
            newColor = contents[i].GetComponent<MeshRenderer>().material.color;
            newColor.r += (targetGrillColor.r - originalColor.r) * grillSpeed * Time.deltaTime;
            newColor.g += (targetGrillColor.g - originalColor.g) * grillSpeed * Time.deltaTime;
            newColor.b += (targetGrillColor.b - originalColor.b) * grillSpeed * Time.deltaTime;
            newColor.a += (targetGrillColor.a - originalColor.a) * grillSpeed * Time.deltaTime;
            contents[i].GetComponent<MeshRenderer>().material.color = newColor;
        }

        cookStatus.cookedTime += Time.deltaTime;
	}
}
