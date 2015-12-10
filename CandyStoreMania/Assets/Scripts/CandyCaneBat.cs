using UnityEngine;
using System.Collections;

public class CandyCaneBat: WeaponsInterface {

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

	public override void upgradeAmmo()
	{

	}
	
	public override void upgradeDamage()
	{
		setBaseDamage(baseDamage * 1.2f);
	}
	
	public override void upgradeFireRate()
	{
		setCooldown(rate * 0.9f);
	}
	
	public override int getAmmo()
	{
		return 0;
	}
	
	public override int getMaxAmmo()
	{
		return 0;
	}
	
	public override float getCooldown()
	{
		return rate;
	}
	
	public override float getBaseDamage()
	{
		return baseDamage;
	}
	
	public override void setAmmo(int a)
	{

	}
	
	public override void setCooldown(float cd)
	{
		cooldown = cd;
		rate = cd;
	}
	
	public override void setBaseDamage(float d)
	{
		baseDamage = d;
	}
	
	public override void reload()
	{

	}
	
	public override void switchToWeapon()
	{
		Debug.Log ("gooo");
		Player.instance.getAnimator ().SetInteger ("weapon", 3);
	}
}
