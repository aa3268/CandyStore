using UnityEngine;
using System.Collections;

public class CandyCaneBat : MonoBehaviour, WeaponsInterface {

	float baseDamage = 15f;
 	float rate = 0.7f;

	float cooldown = 0.7f;
	Animator animator;

	// Use this for initialization
	void Start () {
		animator = Player.instance.getAnimator ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			Fire();
		}
	}

	public void Fire()
	{
		animator.SetBool ("isAttacking", true);
	}

	public void upgradeAmmo()
	{

	}
	
	public void upgradeDamage()
	{
		setBaseDamage(baseDamage * 1.2f);
	}
	
	public void upgradeFireRate()
	{
		setCooldown(rate * 0.9f);
	}
	
	public int getAmmo()
	{
		return 0;
	}
	
	public int getMaxAmmo()
	{
		return 0;
	}
	
	public float getCooldown()
	{
		return rate;
	}
	
	public float getBaseDamage()
	{
		return baseDamage;
	}
	
	public void setAmmo(int a)
	{

	}
	
	public void setCooldown(float cd)
	{
		cooldown = cd;
		rate = cd;
	}
	
	public void setBaseDamage(float d)
	{
		baseDamage = d;
	}
	
	public void reload()
	{

	}
	
	public void switchToWeapon()
	{
		Debug.Log ("gooo");
		Player.instance.getAnimator ().SetInteger ("weapon", 3);
	}
}
