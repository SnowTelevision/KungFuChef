using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedCookAnimation : MonoBehaviour
{
    public CookStatus cookStatus;
    public GameObject finishAnimation;

    public bool isFinishAnimationPlayed;

	// Use this for initialization
	void Start ()
    {
        isFinishAnimationPlayed = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(cookStatus.isGood && !isFinishAnimationPlayed)
        {
            isFinishAnimationPlayed = true;
            playFinishCookAnimation();
        }
	}

    public void playFinishCookAnimation()
    {
        finishAnimation.SetActive(true);
    }


}
