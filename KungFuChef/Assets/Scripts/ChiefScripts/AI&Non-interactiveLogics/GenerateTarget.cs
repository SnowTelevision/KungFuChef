﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GenerateTarget : MonoBehaviour
{
    public GameObject[] targets;
    public Transform player;

    public Quaternion shootDirection;
    public Vector3 generatorEuler;
	// Use this for initialization
	void Start ()
    {
        StartCoroutine(createTarget(3f));
        //print("Targets length: " + targets.Length);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(player);
        generatorEuler = transform.eulerAngles;
        generatorEuler.x = 0;
        generatorEuler.z = 0;
        transform.eulerAngles = generatorEuler;
        shootDirection = transform.rotation;
        shootDirection.eulerAngles = new Vector3(shootDirection.eulerAngles.x, shootDirection.eulerAngles.y, betterRandom(90, -90));
	}

    IEnumerator createTarget(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);



            GameObject newTarget = Instantiate(targets[betterRandom(0, targets.Length - 1)], transform.position, shootDirection);
            newTarget.GetComponent<Rigidbody>().AddForce(newTarget.transform.forward * 3f, ForceMode.Impulse);
            //print("impulse: " + newTarget.name + ": " + newTarget.GetComponent<Rigidbody>().velocity);
            //Destroy(newTarget, 10f);
        }
    }

    #region Better random number generator

    private static readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();

    public static int betterRandom(int minimumValue, int maximumValue)
    {
        byte[] randomNumber = new byte[1];

        _generator.GetBytes(randomNumber);

        double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

        // We are using Math.Max, and substracting 0.00000000001, 
        // to ensure "multiplier" will always be between 0.0 and .99999999999
        // Otherwise, it's possible for it to be "1", which causes problems in our rounding.
        double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);

        // We need to add one to the range, to allow for the rounding done with Math.Floor
        int range = maximumValue - minimumValue + 1;

        double randomValueInRange = Math.Floor(multiplier * range);

        return (int)(minimumValue + randomValueInRange);
    }
    #endregion
}
