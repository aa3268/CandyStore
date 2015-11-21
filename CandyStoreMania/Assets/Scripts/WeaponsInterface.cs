using UnityEngine;
using System.Collections;

public interface WeaponsInterface {

	void upgradeAmmo();
	void upgradeDamage();
	void upgradeFireRate();
	
	int getAmmo();
	int getMaxAmmo();
	float getCooldown();
	float getBaseDamage();
	
	void setAmmo(int a);
	void setCooldown(float cd);
	void setBaseDamage(float d);
	
	void reload();
}
