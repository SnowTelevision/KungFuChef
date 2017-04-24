﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GeneratePlate : MonoBehaviour
{
    public GameObject target;
    //public Transform player;
    public ContainerCounter plateCounter;

    public Quaternion shootDirection;
    // Use this for initialization
    void Start()
    {
        //StartCoroutine(createTarget(10f));
        shootDirection = transform.rotation;
        createTarget();
        //shootDirection.eulerAngles = new Vector3(shootDirection.eulerAngles.x, shootDirection.eulerAngles.y, betterRandom(90, -90));
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(player);
        //shootDirection = transform.rotation;
        //shootDirection.eulerAngles = new Vector3(shootDirection.eulerAngles.x, shootDirection.eulerAngles.y, betterRandom(90, -90));


        //if(plateCounter.containerCount == 0)
        //{
        //    createTarget();
        //}
    }
    public void createTarget()
    {
        GameObject newTarget = Instantiate(target, transform.position, shootDirection);
    }

    //IEnumerator createTarget(float interval)
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(interval);
    //
    //        GameObject newTarget = Instantiate(target, transform.position, shootDirection);
    //        //newTarget.GetComponent<Rigidbody>().AddForce(newTarget.transform.forward * 3f, ForceMode.Impulse);
    //        Destroy(newTarget, 30f);
    //    }
    //}

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