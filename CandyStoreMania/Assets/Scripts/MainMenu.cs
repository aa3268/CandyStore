using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	//public InstructionMenu instructions;
	public GameObject startButton;
	public GameObject settingsButton;
	public GameObject exitButton;
	public GameObject backButton;
	public GameObject instructionsImage;
	public GameObject instructionButton;
	public GameObject logoImage;
	
	public void Start()
	{
		backButton.SetActive (false);
		instructionsImage.SetActive (false);
	}
	
	public void StartLevel(string level)
	{
		Application.LoadLevel (level);
	}
	
	public void SettingsLevel()
	{
		
	}
	
	public void Instructions()
	{
		backButton.SetActive (true);
		instructionsImage.SetActive (true);
		instructionButton.SetActive (false);
		startButton.SetActive (false);
		settingsButton.SetActive (false);
		exitButton.SetActive (false);
		logoImage.SetActive (false);
	}
	
	public void BackButton()
	{
		startButton.SetActive (true);
		settingsButton.SetActive (true);
		exitButton.SetActive (true);
		instructionButton.SetActive (true);
		backButton.SetActive (false);
		instructionsImage.SetActive (false);
		logoImage.SetActive (true);
	}
	
	public void ExitLevel()
	{
		Application.Quit ();
	}
}
