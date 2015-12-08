using UnityEngine;
using System.Collections;

public class Cannon : WeaponsInterface {
	
	
	public int maxAmmo;
	public int ammo;
	public float rate;
	public float damage;
	
	public GameObject bulletPrefab;
	
	public GameObject bulletTemp;

	public Transform nozzle;
	
	public float cooldown;

	public Player player;

	int upgradeStage = 0;
	// Use this for initialization
	void Start () {
		Debug.Log (nozzle);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (player.paused != true) {
			if (Input.GetMouseButtonDown (0)) {
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
			SoundManager.instance.playSound("Cannon");
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
		setAmmo (maxAmmo + 1);
	}
	
	public override void upgradeDamage()
	{
		setBaseDamage(damage * 1.15f);
	}
	
	public override void upgradeFireRate()
	{
		setCooldown(cooldown * 0.9f);
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

	public int upgradeCost()
	{
		return 50 * (upgradeStage + 1);
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
		Player.instance.getAnimator ().SetInteger ("weapon", 2);
	}
}
