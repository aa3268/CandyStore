using UnityEngine;
using System.Collections;

public class LoadLevel:MonoBehaviour {

	public void loadlevel(string level)
	{
		Application.LoadLevel (level);
	}
}
