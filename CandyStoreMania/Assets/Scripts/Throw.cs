using UnityEngine;
using System.Collections;

public class Throw : MonoBehaviour, WeaponsInterface  {
	
	
	public int ammo = 2;
	public float cooldown = 5f;
	public float timer;
<<<<<<< HEAD
	
	public GameObject jarPrefab;
	public GameObject jarTemp;

=======
	public float rate = 5f;
	
>>>>>>> origin/master
	public GameObject bombPrefab;
	public GameObject bombTemp;

	public GameObject jarPrefab;
	public GameObject jarTemp;

	public GameObject left;
	
	public Quaternion rotation;
	public Vector3 position;

<<<<<<< HEAD
	public float power;

	Player player;
=======

	public Player player;
>>>>>>> origin/master
	public int maxAmmo;

	public float power;
	public float multiplier;
	
	// Use this for initialization
	void Start () {
		rotation = new Quaternion (0, 0, 0,0);
		maxAmmo = ammo;
<<<<<<< HEAD
		power = 0f;
		timer = cooldown;
=======
		power = 0;
		timer = cooldown;
		multiplier = 5;

>>>>>>> origin/master
	}
	
	// Update is called once per frame
	void Update () {
		if (Player.instance.paused != true) {
<<<<<<< HEAD
			if (Input.GetMouseButton (0) && ammo > 0 && timer >= cooldown) {
				if(power < 5)
				{
					power += 5 * Time.deltaTime;
				}
			}
			
			if(Input.GetMouseButtonUp(0) && ammo > 0 && timer >= cooldown)
=======
			if (Input.GetMouseButton(1) && ammo > 0) {
				if(power < 5){
					power += multiplier * Time.deltaTime; 
				}
			}
			if(Input.GetMouseButtonUp(1) && ammo > 0)
>>>>>>> origin/master
			{
				Fire ();
				SoundManager.instance.playSound("Caramel");
				power = 0;
			}
			Cooldown ();
<<<<<<< HEAD
			
=======

>>>>>>> origin/master
			if(jarTemp != null)
			{
				if(jarTemp.transform.position.y < 0.7)
				{
<<<<<<< HEAD
					bombTemp = (GameObject)Instantiate (bombPrefab, jarTemp.transform.position, rotation);
=======
					bombTemp = (GameObject) Instantiate(bombPrefab, jarTemp.transform.position, rotation);
>>>>>>> origin/master
					Destroy(jarTemp.gameObject);
					jarTemp = null;
				}
			}
		}
	}
	
	void Fire()
	{
		if (timer >= cooldown) {
<<<<<<< HEAD
			jarTemp = (GameObject)Instantiate (jarPrefab, left.transform.position, rotation);
			
			jarTemp.GetComponent<Rigidbody> ().AddForce (left.transform.forward * power);
=======

			jarTemp = (GameObject)Instantiate (jarPrefab, left.transform.position, rotation);			
			jarTemp.GetComponent<Rigidbody> ().AddForce (left.transform.forward * power);

>>>>>>> origin/master
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
	
	public void upgradeAmmo()
	{
		setAmmo (maxAmmo + 1);
	}
	
	public void upgradeDamage()
	{
		setBaseDamage(0);
	}
	
	public void upgradeFireRate()
	{
		setCooldown(cooldown * 0.9f);
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
		return 0;
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
	}
	
	public void reload()
	{
		ammo = maxAmmo;
	}

	public void switchToWeapon()
	{
		Player.instance.getAnimator ().SetInteger ("weapon", 3);
	}
}