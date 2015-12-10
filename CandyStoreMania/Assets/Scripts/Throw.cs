using UnityEngine;
using System.Collections;

public class Throw : WeaponsInterface  {
	
	
	public int ammo = 2;
	public float cooldown = 5f;
	public float timer;

	public float rate = 5f;
	

	public GameObject bombPrefab;
	public GameObject bombTemp;

	public GameObject jarPrefab;
	public GameObject jarTemp;

	public GameObject left;
	
	public Quaternion rotation;
	public Vector3 position;

	public Player player;
	public int maxAmmo;

	public float power;
	public float multiplier;
	
	// Use this for initialization
	void Start () {
		rotation = new Quaternion (0, 0, 0,0);
		maxAmmo = ammo;

		power = 0;
		timer = cooldown;
		multiplier = 5;

	}
	
	// Update is called once per frame
	void Update () {
		if (Player.instance.paused != true) {

			if (Input.GetMouseButton(0) && ammo > 0) {
				if(power < 5){
					power += multiplier * Time.deltaTime; 
				}
			}
			if(Input.GetMouseButtonUp(0) && ammo > 0 && timer >= cooldown)
			{
				Fire ();
				SoundManager.instance.playSound("Caramel");
				power = 0;
			}
			Cooldown ();

			if(jarTemp != null)
			{
				if(jarTemp.transform.position.y < 0.7)
				{
					bombTemp = (GameObject) Instantiate(bombPrefab, jarTemp.transform.position, rotation);
					Destroy(jarTemp.gameObject);
					jarTemp = null;
				}
			}
		}
	}
	
	void Fire()
	{
		if (timer >= cooldown) {

			jarTemp = (GameObject)Instantiate (jarPrefab, left.transform.position, rotation);			
			jarTemp.GetComponent<Rigidbody> ().AddForce (left.transform.forward * power);

			if (ammo > 0) {
				timer = 0f;
				ammo -= 1;
			}
		}
	}
	
	void Cooldown()
	{
		if (timer < cooldown) {
			timer += Time.deltaTime;
		}
		
	}
	
	public override void upgradeAmmo()
	{
		setAmmo (maxAmmo + 1);
	}
	
	public override void upgradeDamage()
	{
		setBaseDamage(0);
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
		return 0;
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
	}
	
	public override void reload()
	{
		ammo = maxAmmo;
	}

	public override void switchToWeapon()
	{
		Player.instance.getAnimator ().SetInteger ("weapon", 4);
	}
}