using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public void StartLevel()
	{
		Application.LoadLevel ("control_test");
	}

	public void SettingsLevel()
	{

	}

	public void ExitLevel()
	{
		Application.Quit ();
	}
}
