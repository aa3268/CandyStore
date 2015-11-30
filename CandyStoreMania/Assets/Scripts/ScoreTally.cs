using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreTally : MonoBehaviour {

	public static ScoreTally instance;
	public Slider totalHealth;
	public GameObject continueButton;
	public Text currentLevel;
	public Text totalLabel;
	public Text totalMoney;

	public Canvas HealthBarCanvas;
	RectTransform rectTransform;
	public GameObject ammo;
	public GameObject recharge;

	double total;
	bool tallying;

	// Use this for initialization
	void Start () {
		tallying = false;
		total = 0;
		instance = this;
		rectTransform = gameObject.GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (tallying) {
			Tally();
		}
	}

	public void Tally()
	{
		if(totalHealth.value > 20f)
		{
			totalHealth.value = totalHealth.value - 20f;
			total += 0.2 * LevelDirector.instance.getLevel();
			currentLevel.text = total.ToString("C");
		}
		else
		{
			totalHealth.value = 0;
			total += (double)(totalHealth.value / 100) * LevelDirector.instance.getLevel();
			currentLevel.text = total.ToString("C");
			tallying = false;
			LevelDirector.instance.totalScore += total;
			LevelDirector.instance.available += total;

			totalLabel.gameObject.SetActive(true);
			totalMoney.gameObject.SetActive(true);
			totalMoney.text = LevelDirector.instance.totalScore.ToString("C");
			totalHealth.gameObject.SetActive (false);
			continueButton.SetActive(true);

			Time.timeScale = 0.00000000001f;

		}
	}

	public void ScaleUp()
	{
		rectTransform.localScale = new Vector3(1, 1, 1);
		HealthBarCanvas.enabled = false;
		ammo.SetActive (false);
		recharge.SetActive (false);
		Cursor.visible = true;
		tallying = true;
		Player.instance.paused = true;
		Player.instance.enabled = false;
	}

	public void NextMenu()
	{
		totalLabel.gameObject.SetActive(false);
		totalMoney.gameObject.SetActive(false);
		continueButton.SetActive(false);
		rectTransform.localScale = new Vector3(0, 0, 0);
		WeaponsUnit.instance.performChecks ();
		UpgradeMenu.instance.ScaleUp();
	}

}
