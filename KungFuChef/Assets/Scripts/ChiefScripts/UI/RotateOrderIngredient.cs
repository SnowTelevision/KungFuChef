using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOrderIngredient : MonoBehaviour
{
    public float rotateSpeed;
    public bool rotateX;
    public bool rotateY;
    public bool rotateZ;

    public Vector3 localEuler;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        localEuler = transform.localEulerAngles;

		if(rotateX)
        {
            localEuler.x += rotateSpeed * Time.deltaTime;
        }

        if (rotateY)
        {
            localEuler.y += rotateSpeed * Time.deltaTime;
        }

        if (rotateZ)
        {
            localEuler.z += rotateSpeed * Time.deltaTime;
        }

        transform.localEulerAngles = localEuler;
    }
}
