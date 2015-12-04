using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class OpeningDialogue : MonoBehaviour {

	public Text dialogueArea;
	public List<string> script;
	public float displayTime;

	float currentTime = 0f;
	int current = 0;

	// Use this for initialization
	void Start () {
		dialogueArea.text = script [0];
	}
	
	// Update is called once per frame
	void Update () {

		if(current < script.Count - 1)
		{
			if(currentTime >= displayTime)
			{
				current++;
				dialogueArea.text = script[current];
				currentTime = 0;
			}
			else
			{
				currentTime += Time.deltaTime;
			}
		}
	}

	public void reset()
	{
		current = 0;
		dialogueArea.text = script [0];
	}

}
