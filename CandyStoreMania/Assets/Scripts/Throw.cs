using UnityEngine;
using System.Collections;

public class Throw : MonoBehaviour, WeaponsInterface  {
	
	
	public int ammo = 2;
	public float cooldown = 5f;
	public float timer;
	
	
	public GameObject bombPrefab;
	public GameObject bombTemp;
	public GameObject left;
	
	public Quaternion rotation;
	public Vector3 position;
	
	Player player;
	public int maxAmmo;
	float rate = 5f;
	
	
	// Use this for initialization
	void Start () {
		rotation = new Quaternion (0, 0, 0,0);
		maxAmmo = ammo;
	}
	
	// Update is called once per frame
	void Update () {
		if (Player.instance.paused != true) {
			if (Input.GetMouseButtonDown (0)) {
				Fire ();
			}
			Cooldown ();
		}
	}
	
	void Fire()
	{
		bombTemp = (GameObject) Instantiate(bombPrefab, left.transform.position, rotation);
		
		bombTemp.GetComponent<Rigidbody>().AddForce(left.transform.forward * 10f);
		timer = 0f;
		ammo -= 1;
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
}