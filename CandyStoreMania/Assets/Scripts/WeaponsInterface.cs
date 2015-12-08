using UnityEngine;
using System.Collections;

public abstract class WeaponsInterface  : MonoBehaviour
{

	public Sprite associatedImage;
	public abstract void upgradeAmmo();
	public abstract void upgradeDamage();
	public abstract void upgradeFireRate();
	
	public abstract int getAmmo();
	public abstract int getMaxAmmo();
	public abstract float getCooldown();
	public abstract float getBaseDamage();
	
	public abstract void setAmmo(int a);
	public abstract void setCooldown(float cd);
	public abstract void setBaseDamage(float d);
	
	public abstract void reload();
	public abstract void switchToWeapon();
}
