﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	public static PauseMenu instance;
	public Player player;

	public GameObject recharge;
	RectTransform rectTransform;
	public Canvas HealthBarCanvas;
	public GameObject totalHealth;

	public void Start()
	{
		rectTransform = gameObject.GetComponent<RectTransform> ();
		instance = this;
	}
	// Use this for initialization
	public void ScaleDown()
	{
		rectTransform.localScale = new Vector3(0, 0, 0);
		player.paused = false;

		Time.timeScale = 1;
		HealthBarCanvas.enabled = true;
		totalHealth.SetActive (true);
		Cursor.visible = false;
		recharge.SetActive (true);
	}

	public void ScaleUp()
	{
		rectTransform.localScale = new Vector3(1, 1, 1);
		HealthBarCanvas.enabled = false;
		totalHealth.SetActive (false);
		Cursor.visible = true;
		recharge.SetActive (false);
	}

	public void Exit()
	{
		Time.timeScale = 1;
		Application.LoadLevel ("main");
	}
}
