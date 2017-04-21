using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerifyDish : MonoBehaviour
{
    public int tomatoCount;
    public Text scoreText;
    public DisplayFood dishRequirement;

    public int score;
    public GameObject[] toDestroy;

    // Use this for initialization
    void Start ()
    {
        score = 0;
        scoreText.text = score.ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        //print(col.name + " entered");

        if (col.name == "TomatoSliced")
        {
            col.tag = "toDestroy";

        }

        else if (col.name == "Plate(Clone)")
        {
            print(col.GetComponent<VerifyFood>().tomatoCount);

            if (col.GetComponent<VerifyFood>().tomatoCount == tomatoCount)
            {
                dishRequirement.isDishDone = true;
                score++;
                scoreText.text = score.ToString();

                toDestroy = GameObject.FindGameObjectsWithTag("toDestroy");

                for (int i = 0; i < toDestroy.Length; i++)
                {
                    Destroy(toDestroy[i]);
                }

                Destroy(col.gameObject);
            }
        }
    }
}
