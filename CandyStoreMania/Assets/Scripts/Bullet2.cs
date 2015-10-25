using UnityEngine;
using System.Collections;

public class Bullet2 : MonoBehaviour {

	public float health = 2.5f;
	public float timer = 1.5f;
	// Use this for initialization
	void Update()
	{
		Kill ();
		Timer ();
	}
	void Timer()
	{
		timer -= Time.deltaTime;
		if (timer < 0) {
			gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
	}
	void Kill()
	{
		health -= Time.deltaTime;
		if (health < 0) {
			Destroy(gameObject);
		}
	}
}
