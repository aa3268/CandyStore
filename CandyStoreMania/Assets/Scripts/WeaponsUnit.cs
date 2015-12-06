using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WeaponsUnit : MonoBehaviour {

	public static WeaponsUnit instance;
	public List<GameObject> weaponsUnits;
	public List<UpgradeUnitBehavior> unitBehavior;
	public GameObject next;
	public GameObject previous;

	RectTransform trans;

	int current=0;


	// Use this for initialization
	void Start () {
		instance = this;
		UpgradeUnitBehavior b;
		foreach(GameObject units in weaponsUnits)
		{
			b = units.GetComponent<UpgradeUnitBehavior>();
		}

		for(int i  = 0; i < unitBehavior.Count; i++)
		{
			unitBehavior[i].gameObject.SetActive(false);
		}
		unitBehavior [current].gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
	}


	public void performChecks()
	{
		foreach(UpgradeUnitBehavior unit in unitBehavior)
		{
			unit.checkUnlockable();
			unit.checkUpgradable();
		}

		if(current >= weaponsUnits.Count-1)
		{
			next.SetActive(false);
		}
		else
		{
			next.SetActive(true);
		}
		if(current <= 0)
		{
			previous.SetActive(false);
		}
		else
		{
			previous.SetActive(true);
		}

	}

	public void setUnlockedWeapons()
	{
		foreach(UpgradeUnitBehavior units in unitBehavior)
		{
			if(units.unlockStatus)
			{
				Player.instance.addWeapon(units.associatedWeapon, units.associatedNum);

			}
		}
	}

	public void SnapTo(int i)
	{
		current += i;
		unitBehavior [current].gameObject.SetActive (true);
		unitBehavior [current-i].gameObject.SetActive (false);
		performChecks ();
	}
}
