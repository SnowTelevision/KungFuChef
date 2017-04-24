using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DisplayFood : MonoBehaviour
{
    //public List<GameObject> foodTypes = new List<GameObject>();
    public GameObject[] foodTypes;
    public int dishTimeLimit;
    public VerifyDish dishVerifier;

    public bool isDishDone;
    public GameObject newDish;
    public Coroutine displayDish;
    public Quaternion zeroRotation = new Quaternion();
    public int tomatoCount;

    // Use this for initialization
    void Start ()
    {
        isDishDone = false;
        displayDish = StartCoroutine(displayDishRoutine(dishTimeLimit));
        zeroRotation.eulerAngles = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(isDishDone)
        {
            StopCoroutine(displayDish);
            
            Destroy(newDish);
            displayDish = StartCoroutine(displayDishRoutine(dishTimeLimit));
            isDishDone = false;
        }
	}

    IEnumerator displayDishRoutine(float timeLimit)
    {
        tomatoCount = betterRandom(0, foodTypes.Length - 1);
        //print("Display");
        newDish = Instantiate(foodTypes[tomatoCount]);
        newDish.transform.position = new Vector3(0.85f, 2, 6);
        newDish.transform.eulerAngles = new Vector3(180, 0, 180);
        //print("Instantiated");
        dishVerifier.tomatoCount = tomatoCount + 1;

        yield return new WaitForSeconds(timeLimit);

        dishVerifier.score--;
        dishVerifier.scoreText.text = dishVerifier.score.ToString();
        Destroy(newDish);
        
        displayDish = StartCoroutine(displayDishRoutine(dishTimeLimit));
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
