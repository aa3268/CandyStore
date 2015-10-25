using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float health = 5f;
	// Use this for initialization
	void Update()
	{
		StopParticle ();
		Kill ();
	}

	void StopParticle()
	{
		if (Mathf.Floor (GetComponent<Transform> ().position.y) == 0) {
			GetComponent<ParticleSystem>().loop = false;
		}
	}

	void Kill()
	{
		health -= Time.deltaTime;
		if (health < 0) {
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision other)
	{
			Destroy(gameObject);
	}
}
