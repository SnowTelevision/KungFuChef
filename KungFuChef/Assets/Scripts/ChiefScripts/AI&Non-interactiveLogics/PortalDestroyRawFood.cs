﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDestroyRawFood : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Food")
        {
            Destroy(col.gameObject);
        }
    }
}
