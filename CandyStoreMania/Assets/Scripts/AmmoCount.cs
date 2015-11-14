using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour {

	public Text ammoLeft;
	public Gradient color;
	public Animator animator;

	// Use this for initialization
	void Start () {
		ammoLeft.text = "Ammo: ";
	}
	
	// Update is called once per frame
	void Update () {
		ammoLeft.text = "Ammo: " + Player.instance.getAmmoCount ();
		float val = ((float) Player.instance.getAmmoCount())/Player.instance.getMaxAmmo();
		ammoLeft.color = color.Evaluate(val);

		if(val < 0.4)
		{
			if(!animator.GetBool("Critical"))
			{
				animator.SetBool("Critical", true);
			}
		}
		else
		{
			if(animator.GetBool("Critical"))
			{
				animator.SetBool("Critical", false);
			}
		}
	}

}
