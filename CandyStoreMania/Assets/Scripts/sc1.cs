using UnityEngine;
using System.Collections;

public class sc1 : MonoBehaviour {


	public bool moveit;

	bool moveup;
	bool movedown;

	void Start()
	{
		moveup = true;
		movedown = false;

		moveit = false;
	}

	void Update()
	{
		if (moveit) {
			move ();
		}

	}
	public void move()
	{
		if (moveup) {
			if (Mathf.Round (transform.position.y) <= 4.5) {
				transform.Translate (Vector3.up * 0.1f);
			}

			if(Mathf.Round(transform.position.y) >= 4.5)
			{
				movedown = true;
				moveup = false;
			}
		}

		if (movedown) {
			if (Mathf.Round (transform.position.y) > 1) {
				transform.Translate (Vector3.down * 0.1f);
			}

			if(Mathf.Round(transform.position.y) <= 1)
			{
				moveup = true;
				movedown = false;
			}
		}
	}
}
