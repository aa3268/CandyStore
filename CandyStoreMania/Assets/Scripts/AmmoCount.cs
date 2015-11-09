using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour {

	public Text ammoLeft;

	// Use this for initialization
	void Start () {
		ammoLeft.text = "Ammo: ";
	}
	
	// Update is called once per frame
	void Update () {
		ammoLeft.text = "Ammo: " + Player.instance.getAmmoCount ();
	}
}
