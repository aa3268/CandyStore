using UnityEngine;
using System.Collections;

public class UpgradeMenu : MonoBehaviour {


	public Camera pause;
	public Camera upgrade;
	// Update is called once per frame
	public void Unlock () {
		if (Cursor.visible) {
			Player.gunTwoSelectable = true;
		}
	}

	public void Back()
	{
		if (Cursor.visible) {
			upgrade.enabled = false;
			pause.enabled = true;
		}
	}
}
