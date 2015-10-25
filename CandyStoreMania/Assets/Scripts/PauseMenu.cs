using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {


	public Camera pauseCam;
	public Camera playerCam;
	public Camera upgradeCam;

	public Player player;
	// Use this for initialization
	public void Resume()
	{
		if (Cursor.visible) {
			if (pauseCam.enabled == true) {
				playerCam.enabled = true;
				player.pause.enabled = false;
				Time.timeScale = 1;
			}


		}
	}



	public void Exit()
	{
		if (Cursor.visible) {
			Application.LoadLevel (0);
		}
	}


	public void Upgrade()
	{
		if (Cursor.visible) {
			if (pauseCam.enabled == true) {
				upgradeCam.enabled = true;
				pauseCam.enabled = false;

			}
			
			
		}

	}
}
