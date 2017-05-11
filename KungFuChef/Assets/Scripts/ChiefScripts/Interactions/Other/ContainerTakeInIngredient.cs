using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerTakeInIngredient : MonoBehaviour
{
    public GameProgressManager gameManager;

    public List<Transform> containedIngredientTransforms;
    public CookStatus[] containedIngredientStatuses;
    public int ingredientCount;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        //print(col.tag + " entered");

        if (col.tag == "Food")
        {
            //print(col.transform.localPosition);
            col.transform.SetParent(transform);
            //print(col.transform.localPosition + ", " + col.transform.parent.name);
            containedIngredientTransforms.Add(col.transform);
            ingredientCount++;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Food")
        {
            print(col.name + " exited");

            col.transform.parent = null;

            containedIngredientTransforms.Remove(col.transform);
            ingredientCount--;
        }
    }

    public void arrangeOrder()
    {
        //containedIngredientTransforms = GetComponentsInChildren<Transform>();
        containedIngredientStatuses = new CookStatus[ingredientCount];

        //List<Transform> tempSortList = new List<Transform>(GetComponentsInChildren<Transform>());
        //tempSortList.Sort((x, y) => { return Mathf.FloorToInt(1000.0f * (x.position.z - y.position.z)); });
        for(int i = 0; i < containedIngredientTransforms.Count; i++)
        {
            containedIngredientTransforms[i].SetParent(transform);
        }

        containedIngredientTransforms.Sort((x, y) => { return Mathf.FloorToInt(100000.0f * (x.localPosition.z - y.localPosition.z)); });
        //containedIngredientTransforms = tempSortList.ToArray();

        for (int i = 0; i < ingredientCount; i++)
        {
            print("Ingredient " + i + "'s local position:" + containedIngredientTransforms[i].localPosition * 10000f);
            containedIngredientStatuses[i] = containedIngredientTransforms[i].GetComponent<CookStatus>();
        }

        gameManager.orderVerifier.currentContainedIngredients = containedIngredientStatuses;

        gameManager.orderVerifier.startVerifyOrder();
    }


    public IEnumerator waitAndExecute()
    {
        yield return new WaitForSeconds(1f);
    }
}
