using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {
	
	
	public int ammo;
	public float rate;
	public float damage;
	
	public GameObject bulletPrefab;
	
	public GameObject bulletTemp;
	public GameObject nozzle;
	
	public float cooldown;

	public GameObject player;
	
	// Use this for initialization
	void Start () {
		ammo = 1000;
		rate = 5;
		damage = 20;
		cooldown = 5;
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (player.GetComponent<Player> ().paused != true) {
			if (Input.GetMouseButtonDown (0)) {
				Fire ();
			}
			Cooldown ();
		}
	}
	
	
	void Fire()
	{
		if (ammo > 0 && cooldown >= rate) {
			Vector3 pos = nozzle.GetComponent<Transform> ().position;
			Quaternion rot = new Quaternion (0, 0, 0, 0);
			bulletTemp = (GameObject)Instantiate (bulletPrefab, pos, rot);
			bulletTemp.GetComponent<Rigidbody> ().AddForce (nozzle.transform.forward * 15f);
			cooldown = 0;
			ammo--;
		}
	}
	
	void Cooldown()
	{
		if (cooldown < rate) {
			cooldown += Time.deltaTime;
		}
		
	}
}
