using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour {

	public static UpgradeMenu instance;
	public Player player;
	public Text score;
	public Text available;

	public GameObject unlockButton;
	public GameObject Gun2;
	RectTransform rectTransform;
	bool unlocked = false;

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
		Cursor.visible = true;
		Time.timeScale = 1;
	}
	
	public void ScaleUp()
	{
		rectTransform.localScale = new Vector3(1, 1, 1);
		setScore ();
		checkUnlockable ();
	}
	
	public void Exit()
	{
		if (Cursor.visible) 
		{
			Application.LoadLevel ("main");
		}
	}

	public void setScore()
	{
		score.text = "Total score: " + LevelDirector.instance.getScore ();
		available.text = "Points available: " + LevelDirector.instance.getAvailablePoints ();
	}

	public void checkUnlockable()
	{
		if(LevelDirector.instance.getAvailablePoints() >= 150 && !unlocked)
		{
			unlockButton.SetActive(true);
			unlocked = true;
		}
		else
		{
			unlockButton.SetActive(false);
		}
	}

	public void unlock()
	{
		LevelDirector.instance.purchase (150);
		available.text = "Points available: " + LevelDirector.instance.getAvailablePoints ();
		//GameObject gun2 = (GameObject)Instantiate (Resources.Load ("prefabs/Gun2"));
		Player.instance.addWeapon (Gun2);
	}
}
