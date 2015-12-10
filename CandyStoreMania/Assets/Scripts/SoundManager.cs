using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

    Dictionary<string, AudioClip> AudioClips;
	AudioSource player;

	public List<string> AudioNames;
	public List<AudioClip> Clips;
	public static SoundManager instance;

	// Use this for initialization
	void Start () {
	
		player = GetComponent<AudioSource> ();
		instance = this;
		AudioClips = new Dictionary<string, AudioClip> ();

		if (AudioNames.Count != Clips.Count) 
		{
			Debug.Log("Error! AudioNames and AudioClips different sizes!");
		}
		for(int i = 0; i < AudioNames.Count; i++)
		{
			AudioClips.Add(AudioNames[i], Clips[i]);
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playSound(string clip)
	{
		player.PlayOneShot(AudioClips[clip]);
	}
}
