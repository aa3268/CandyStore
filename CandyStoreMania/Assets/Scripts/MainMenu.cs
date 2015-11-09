using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public void StartLevel(string level)
	{
		Application.LoadLevel (level);
	}

	public void SettingsLevel()
	{

	}

	public void ExitLevel()
	{
		Application.Quit ();
	}
}
