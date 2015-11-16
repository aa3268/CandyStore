using UnityEngine;
using System.Collections;

public class Bullet_Cannon : MonoBehaviour {

	public float health = 2.5f;
	public float timer = 1.5f;

	public ParticleSystem particles;

	void Start()
	{
		particles = GetComponent<ParticleSystem> ();
	}

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


	void OnParticleCollision(GameObject obj)
	{
		if(obj.gameObject.name.Contains("Enemy"))
		{
			obj.GetComponent<EnemyScript>().getHealthBar().doDamage(50);
			obj.GetComponent<EnemyScript>().reactToBullet();
		}

	}
}
