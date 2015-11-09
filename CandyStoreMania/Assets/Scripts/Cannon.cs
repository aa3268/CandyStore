using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour, WeaponsInterface {
	
	
	public int maxAmmo;
	public int ammo;
	public float rate;
	public float damage;
	
	public GameObject bulletPrefab;
	
	public GameObject bulletTemp;
	public GameObject nozzle;
	
	public float cooldown;

	public GameObject player;

	int upgradeStage = 0;
	// Use this for initialization
	void Start () {

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

	
	public void upgrade()
	{
		if(upgradeStage % 3 == 0)
		{
			upgradeStage++;
			setCooldown(cooldown * 0.9f);
		}
		else if(upgradeStage % 3 == 1)
		{
			upgradeStage++;
			setAmmo(maxAmmo + 1);
		}
		else if(upgradeStage % 3 == 2)
		{
			upgradeStage++;
			setBaseDamage(damage * 1.1f);
		}
	}
	
	public int getAmmo()
	{
		return ammo;
	}

	public int getMaxAmmo()
	{
		return maxAmmo;
	}
	
	public float getCooldown()
	{
		return rate;
	}
	
	public float getBaseDamage()
	{
		return damage;
	}

	public int upgradeCost()
	{
		return 50 * (upgradeStage + 1);
	}

	public void setAmmo(int a)
	{
		ammo = a;
		maxAmmo = ammo;
	}
	
	public void setCooldown(float cd)
	{
		cooldown = cd;
		rate = cd;
	}
	
	public void setBaseDamage(float d)
	{
		damage = d;
	}

	public void reload()
	{
		ammo = maxAmmo;
	}
}
