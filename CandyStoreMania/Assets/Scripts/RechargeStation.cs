﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RechargeStation : MonoBehaviour {

	public Text text;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void reload(GameObject weapon)
	{
		weapon.GetComponent<WeaponsInterface> ().reload ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.name.Contains ("Player")) {

			text.text = "RECHARGE [space]";
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.name.Contains ("Player")) {
			text.text = "";
		}
	}
}
