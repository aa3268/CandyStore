using UnityEngine;
using System.Collections;

public class Bullet_GumballGun : MonoBehaviour {

	public float health = 5f;
	public float damage = 20;
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
		if(other.gameObject.name.Contains("Enemy"))
		{
			other.gameObject.GetComponent<EnemyScript>().reactToBullet(Player.instance.getBaseDamage());
			Destroy (gameObject);
		}

		if (!other.gameObject.name.Contains ("Player")) {
			Destroy(gameObject);
		}

	}
}
