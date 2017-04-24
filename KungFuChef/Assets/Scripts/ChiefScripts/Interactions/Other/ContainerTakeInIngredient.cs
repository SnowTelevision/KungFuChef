using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerTakeInIngredient : MonoBehaviour
{


    public Transform[] containedIngredientTransforms;
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
        //print(col.name + " entered");

        if (col.tag == "Food")
        {
            col.transform.parent = transform.parent;
            ingredientCount++;
        }
    }

    void OnTriggerExit(Collider col)
    {
        //print(col.name + " exited");

        if (col.tag == "Food")
        {
            col.transform.parent = null;
            ingredientCount--;
        }
    }

    public void arrangeOrder()
    {
        containedIngredientTransforms = GetComponentsInChildren<Transform>();
        containedIngredientStatuses = new CookStatus[ingredientCount];

        List<Transform> tempSortList = new List<Transform>(GetComponentsInChildren<Transform>());
        tempSortList.Sort((x, y) => { return Mathf.FloorToInt(1000.0f * (x.position.z - y.position.z)); });
        containedIngredientTransforms = tempSortList.ToArray();

        for(int i = 0; i < ingredientCount; i++)
        {
            containedIngredientStatuses[i] = containedIngredientTransforms[i].GetComponent<CookStatus>();
        }
    }

    public IEnumerator waitAndExecute()
    {
        yield return new WaitForSeconds(1f);
    }
}
