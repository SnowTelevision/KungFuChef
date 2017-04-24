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
            if(orderArray[i] == 0)
            {
                continue;
            }

            currentIngredientCount = orderArray[i];

            for(int j = ingredientArrayIndex; j < currentIngredientCount; j++)
            {
                if(currentContainedIngredients[ingredientArrayIndex].foodType != i || !currentContainedIngredients[ingredientArrayIndex].isGood || ingredientArrayIndex >= currentContainedIngredients.Length)
                {
                    isOrderCorrect = false;
                    break;
                }

                ingredientArrayIndex++;
            }

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
