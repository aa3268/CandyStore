using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeMenu : MonoBehaviour {

	public static UpgradeMenu instance;
	public Player player;
	public Text score;
	public Text available;

	public Canvas HealthBarCanvas;
	RectTransform rectTransform;
	public GameObject ammo;
	public GameObject recharge;
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
		ammo.SetActive (true);
		HealthBarCanvas.enabled = true;
		recharge.SetActive (true);
		EventSystem.current.SetSelectedGameObject (null);
	}
	
	public void ScaleUp()
	{
		rectTransform.localScale = new Vector3(1, 1, 1);
		HealthBarCanvas.enabled = false;
		ammo.SetActive (false);
		recharge.SetActive (false);
		setScore ();
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
		score.text = "" + LevelDirector.instance.getScore ();
		available.text = "" + LevelDirector.instance.getAvailablePoints ();
	}


}
