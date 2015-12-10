using UnityEngine;
using System.Collections;

public class Bullet_Cannon : MonoBehaviour {

	public float health = 3.5f;
	public float timer = 1.5f;
	public int damage;

	public ParticleSystem particles;
	public MeshRenderer ren;

	public bool played;

	void Start()
	{
		played = false;
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
			ren.enabled = false;
			particles.transform.parent = null;

			if(!played)
			{
				particles.Play();
			}
			played = true;

		}
	}
	void Kill()
	{
		health -= Time.deltaTime;
		if (health < 0) {
			Destroy(particles.gameObject);
			Destroy(gameObject);
		}
	}
	void OnCollisionEnter(Collision obj)
	{
		if(obj.gameObject.name.Contains("Enemy"))
		{
			timer = -1;	
		}
	}
}
