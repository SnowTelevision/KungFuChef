using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifyOrder : MonoBehaviour
{
    public GameProgressManager gameManager;

    public int[] orderArray;
    public CookStatus[] currentContainedIngredients;
    public bool isOrderCorrect;

    // Use this for initialization
    void Start ()
    {
        isOrderCorrect = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider col)
    {
        //print(col.name + " entered");

        if (col.tag == "FoodContainer")
        {
            gameManager.currentOrder = this;
            isOrderCorrect = true;
            orderArray = gameManager.orderDisplay.orderArray;
            col.GetComponentInChildren<ContainerTakeInIngredient>().arrangeOrder();
        }
    }

    public void startVerifyOrder()
    {
        int ingredientArrayIndex = 0;
        int currentIngredientCount = 0;

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
                    isOrderCorrect = false;
                    break;
                }

                ingredientArrayIndex++;
            }

            print("next type");

            if(!isOrderCorrect)
            {
                break;
            }
        }

        if (isOrderCorrect)
        {
            print("CORRECT!");
            isOrderCorrect = false;
        }
    }
}
