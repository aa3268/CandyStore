using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionBehavior : MonoBehaviour {

	public string key;
	Toggle toggle;
	public RectTransform rectTransform;
	bool setUp;

	// Use this for initialization
	void Start () {
		setUp = false;
		toggle = GetComponent<Toggle> ();
		if (PlayerPrefs.GetString (key) == null) {
			toggle.isOn = false;
			PlayerPrefs.SetString(key, "Off");
			setUp = true;
		}
		else 
		{
			if (PlayerPrefs.GetString (key).Equals("On")) {
				toggle.isOn = true;
				setUp = true;
			}
			else
			{
				toggle.isOn = false;
				setUp = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ToggleSetting()
	{
		Debug.Log ("toggle");
		if (setUp) {
			if (PlayerPrefs.GetString (key) != null) {
				if (PlayerPrefs.GetString (key).Equals ("On")) {
					PlayerPrefs.SetString (key, "Off");
				} else {
					PlayerPrefs.SetString (key, "On");
				}
			}
		}
	}

	public void ScaleDown()
	{
		rectTransform.localScale = new Vector3(0, 0, 0);
	}
	
	public void ScaleUp()
	{
		rectTransform.localScale = new Vector3(1, 1, 1);
	}
}
