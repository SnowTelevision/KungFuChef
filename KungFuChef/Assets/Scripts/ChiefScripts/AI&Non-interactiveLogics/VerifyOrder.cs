using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerifyOrder : MonoBehaviour
{
    public GameProgressManager gameManager;
    public Text scoreText;

    public int[] orderArray;
    public CookStatus[] currentContainedIngredients;
    public bool isOrderCorrect;
    public GameObject thisContainer;

    // Use this for initialization
    void Start ()
    {
        isOrderCorrect = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		//if(Time.time - gameManager.currentOrderStartTime >= gameManager.orderTimeLimit)
  //      {
  //          scoreText.text = "Time Out!";
  //      }
	}

    void OnTriggerEnter(Collider col)
    {
        //print(col.name + " entered");

        if (col.tag == "FoodContainer")
        {
            gameManager.orderVerifier = this;
            isOrderCorrect = true;
            orderArray = gameManager.orderDisplay.orderArray;
            thisContainer = col.gameObject;
            col.GetComponentInChildren<ContainerTakeInIngredient>().arrangeOrder();
        }

        if (col.tag == "Food")
        {
            scoreText.text = "No Container!";
            Destroy(col.gameObject);
        }
    }

    public void startVerifyOrder()
    {
        int ingredientArrayIndex = 0;
        int currentIngredientCount = 0;

        if(currentContainedIngredients.Length == 0)
        {
            isOrderCorrect = false;
            scoreText.text = "Empty Plate!";

            if (thisContainer != null)
            {
                Destroy(thisContainer);
            }

            return;
        }

        for(int i = 0; i < orderArray.Length; i++)
        {
            print("orderType: " + i);

            if(orderArray[i] == 0)
            {
                print("Type " + i + " is not needed.");
                continue;
            }

            currentIngredientCount = orderArray[i];

            for(int j = 0; j < currentIngredientCount; j++)
            {
                print("Contained ingredient index: " + ingredientArrayIndex + ", type is: " + currentContainedIngredients[ingredientArrayIndex].foodType);
                if(currentContainedIngredients[ingredientArrayIndex].foodType != i || !currentContainedIngredients[ingredientArrayIndex].isGood || ingredientArrayIndex >= currentContainedIngredients.Length)
                {
                    print("WRONG Contained ingredient index: " + ingredientArrayIndex + ", type is: " + currentContainedIngredients[ingredientArrayIndex].foodType);
                    scoreText.text = "Wrong!";
                    isOrderCorrect = false;
                    break;
                }

                ingredientArrayIndex++;
            }

            print("next type");

            if(!isOrderCorrect)
            {
                if (thisContainer != null)
                {
                    Destroy(thisContainer);
                }

                break;
            }
        }

        if (isOrderCorrect)
        {
            print("CORRECT!");
            scoreText.text = "Correct!";
            gameManager.orderDisplay.displayOrderFunc();
            gameManager.currentOrderStartTime = Time.time;
            gameManager.moneyScore += (currentContainedIngredients.Length + 1) * 10;

            if (thisContainer != null)
            {
                Destroy(thisContainer);
            }

            isOrderCorrect = false;
        }
    }
}
