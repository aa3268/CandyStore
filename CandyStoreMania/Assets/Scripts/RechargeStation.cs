using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RechargeStation : MonoBehaviour {

	public Text text;
	Camera cam;

	// Use this for initialization
	void Start () {
		cam = GameObject.Find ("PlayerCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 screenPos = cam.WorldToScreenPoint (transform.position);
		
		text.transform.position = new Vector3 (screenPos.x, screenPos.y, screenPos.z);
		Debug.Log (transform.position);
	}

	public void reload(GameObject weapon)
	{
		weapon.GetComponent<WeaponsInterface> ().reload ();
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("hi");
		if (other.name.Contains ("Player")) {

			text.text = "RECHARGE [space]";
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.name.Contains ("Player")) {
			Debug.Log ("Exit!");
			text.text = "";
		}
	}
}
