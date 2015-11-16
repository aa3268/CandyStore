using UnityEngine;
using System.Collections;

public interface WeaponsInterface {

	void upgrade();
	int getAmmo();
	int getMaxAmmo();
	float getCooldown();
	float getBaseDamage();
	int upgradeCost();

	void setAmmo(int a);
	void setCooldown(float cd);
	void setBaseDamage(float d);

	void reload();
}
