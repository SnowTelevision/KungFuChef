using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System;
using UnityEngine.UI;

public class DisplayOrder : MonoBehaviour
{
    public GameObject[] bunTypes;
    public GameObject[] meatTypes;
    public GameObject tomato;
    public GameObject onion;
    public GameObject cheese;
    public GameObject bacon;
    public GameObject lettuce;
    public int numberOfIngredientTypes;
    public Text[] ingredientCounters;

    public int[] orderArray;

    // Use this for initialization
    void Start ()
    {
        orderArray = new int[numberOfIngredientTypes];
        //displayOrderFunc();
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void displayOrderFunc()
    {
        
        for(int i = 0; i < numberOfIngredientTypes; i++)
        {
            if(i == 0)
            {
                orderArray[i] = 1;
            }

            if(i == 1)
            {
                orderArray[i] = betterRandom(1, 2);
            }

            if (i == 2)
            {
                orderArray[i] = betterRandom(0, 1);
            }

            if (i == 3)
            {
                orderArray[i] = betterRandom(0, 1);
            }

            if (i == 4)
            {
                orderArray[i] = 1;
            }

            ingredientCounters[i].text = orderArray[i].ToString();
        }
        

        /////Testing
        //for (int i = 0; i < numberOfIngredientTypes; i++)
        //{
        //    if (i == 0)
        //    {
        //        orderArray[i] = 1;
        //    }

        //    if (i == 1)
        //    {
        //        orderArray[i] = 2;
        //    }

        //    if (i == 2)
        //    {
        //        orderArray[i] = 1;
        //    }

        //    if (i == 3)
        //    {
        //        orderArray[i] = 0;
        //    }

        //    if (i == 4)
        //    {
        //        orderArray[i] = 1;
        //    }
        //}
        ///
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
