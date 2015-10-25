using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Splash : MonoBehaviour {

	private bool fadeOut;
	private bool fadeIn;
	private Color c;

	public Text text;
	public float speed;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		fadeOut = true;
		fadeIn = false;
	}
	
	// Update is called once per frame
	void Update () {
		ToMain ();
		Fade ();
	}
	void ToMain()
	{
		if (Input.anyKey) {
			Application.LoadLevel(1);
		}
	}
	void Fade()
	{
		c = text.color;

		if(c.a > 0 && fadeOut)
		{
			c.a = c.a - speed * Time.deltaTime;
			if(c.a <= 0)
			{
				fadeOut = false;
				fadeIn = true;
			}
		}
		if(c.a < 1 && fadeIn)
		{
			c.a = c.a + speed * Time.deltaTime;
			if(c.a >= 1)
			{
				fadeOut = true;
				fadeIn = false;
			}
		}
		
		text.color = c;
	}
}
