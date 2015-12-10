using UnityEngine;
using System.Collections;

public class AnimatorFunctions : MonoBehaviour {

	Animator animator;
	public void Start()
	{
		animator = gameObject.GetComponent<Animator> ();
	}

	public void stopAttacking()
	{
		animator.SetBool ("isAttacking", false);
	}
}
