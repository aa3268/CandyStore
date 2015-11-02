using UnityEngine;
using System.Collections;

public class UpgradeMenu : MonoBehaviour {

	public static UpgradeMenu instance;
	public Player player;
	
	RectTransform rectTransform;
	
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
	}
	
	public void Exit()
	{
		if (Cursor.visible) 
		{
			Application.LoadLevel ("main");
		}
	}
}
