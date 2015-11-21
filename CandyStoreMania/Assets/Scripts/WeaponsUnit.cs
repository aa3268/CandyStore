using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponsUnit : MonoBehaviour {

	public static WeaponsUnit instance;
	public List<GameObject> weaponsUnits;
	public List<UpgradeUnitBehavior> unitBehavior;
	RectTransform trans;

	// Use this for initialization
	void Start () {
		instance = this;
		trans = GetComponent<RectTransform> ();
		trans.anchorMax = new Vector3 (weaponsUnits.Count, 1f);
		placeUnits ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void placeUnits()
	{
		RectTransform unitTransform;
		UpgradeUnitBehavior b;
		int i = 0;
		float distance = 1f / weaponsUnits.Count;
		foreach(GameObject units in weaponsUnits)
		{
			unitTransform = units.GetComponent<RectTransform>();

			unitTransform.anchorMin = new Vector2(i * distance, 0.05f);
			unitTransform.anchorMax = new Vector2((i+1)*distance, 0.95f);
			i++;

			b = units.GetComponent<UpgradeUnitBehavior>();
			unitBehavior.Add(b);

		}
	}

	public void performChecks()
	{
		foreach(UpgradeUnitBehavior unit in unitBehavior)
		{
			unit.checkUnlockable();
			unit.checkUpgradable();
		}
	}

	public void setUnlockedWeapons()
	{
		foreach(UpgradeUnitBehavior units in unitBehavior)
		{
			if(units.unlockStatus)
			{
				Player.instance.addWeapon(units.associatedWeapon);
			}
		}
	}
}
