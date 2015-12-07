using UnityEngine;
using System.Collections;

public class DisplayBehavior : WindowBehavior {

	public GameObject baseObject;

	public override void changeStage()
	{
		if(currentStage < 2)
		{
			currentStage++;
		}

		if(currentStage == 1)
		{
			baseObject.SetActive(false);
			if(firstStage != null)
			{
				firstStage.SetActive(true);
			}
		}
		else if(currentStage == 2)
		{
			if(firstStage != null && secondStage != null)
			{
				firstStage.SetActive(false);
				secondStage.SetActive(true);
			}
		}
	}

	public override void reset()
	{
		currentStage = 0;
		baseObject.SetActive (true);
		if (secondStage != null) {
			secondStage.SetActive (false);
		}
	}
}
