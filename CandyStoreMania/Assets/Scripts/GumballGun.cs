using UnityEngine;
using System.Collections;

public class GumballGun : WeaponsInterface {
	
	
	public int ammo;
	public float rate;
	public float damage;
	
	public GameObject bulletPrefab;
	
	public GameObject bulletTemp;
	public Transform nozzle;
	
	public float cooldown;
	
	int upgradeStage;
	
	public Player player;
	int maxAmmo;
	
	// Use this for initialization
	void Start () {
		ammo = 15;
		maxAmmo = ammo;
		rate = 1;
		damage = 20;
		upgradeStage = 0;
		cooldown = 1f;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (player.paused != true) {
			if (Input.GetMouseButton (0)) {
				Fire ();
			}
			Cooldown ();
		}
	}
	
	
	void Fire()
	{
		if (ammo > 0 && cooldown >= rate) {
			Vector3 pos = nozzle.position;
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
	
	public override void upgradeAmmo()
	{
		setAmmo (maxAmmo + 5);
	}
	
	public override  void upgradeDamage()
	{
		setBaseDamage(damage * 1.1f);
	}
	
	public override void upgradeFireRate()
	{
		setCooldown(cooldown * 0.85f);
	}
	
	public override int getAmmo()
	{
		return ammo;
	}
	
	public override int getMaxAmmo()
	{
		return maxAmmo;
	}
	
	public override float getCooldown()
	{
		return rate;
	}
	
	public override float getBaseDamage()
	{
		return damage;
	}
	
	public override void setAmmo(int a)
	{
		ammo = a;
		maxAmmo = ammo;
	}
	
	public override void setCooldown(float cd)
	{
		cooldown = cd;
		rate = cd;
	}
	
	public override void setBaseDamage(float d)
	{
		damage = d;
	}
	
	public override void reload()
	{
		ammo = maxAmmo;
	}

	public override void switchToWeapon()
	{
		Player.instance.getAnimator ().SetInteger ("weapon", 1);
	}
}
