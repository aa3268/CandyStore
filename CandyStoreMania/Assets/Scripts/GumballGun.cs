using UnityEngine;
using System.Collections;

public class GumballGun : MonoBehaviour, WeaponsInterface {
	
	
	public int ammo;
	public float rate;
	public float damage;
	
	public GameObject bulletPrefab;
	
	public GameObject bulletTemp;
	public GameObject nozzle;
	
	public float cooldown;
	
	int upgradeStage;
	
	public GameObject player;
	int maxAmmo;
	
	// Use this for initialization
	void Start () {
		ammo = 15;
		maxAmmo = ammo;
		rate = 1;
		damage = 20;
		upgradeStage = 0;
		cooldown = 1f;
		
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (player.GetComponent<Player> ().paused != true) {
			if (Input.GetMouseButton (0)) {
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
			
			SoundManager.instance.playSound("Bubblegum");
		}
	}
	
	void Cooldown()
	{
		if (cooldown < rate) {
			cooldown += Time.deltaTime;
		}
		
	}
	
	public void upgradeAmmo()
	{
		setAmmo (maxAmmo + 5);
	}
	
	public void upgradeDamage()
	{
		setBaseDamage(damage * 1.1f);
	}
	
	public void upgradeFireRate()
	{
		setCooldown(cooldown * 0.85f);
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

	public void switchToWeapon()
	{
		Player.instance.getAnimator ().SetInteger ("weapon", 1);
	}
}
