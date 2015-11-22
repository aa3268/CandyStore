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
	public ScrollRect scrollRect;

	RectTransform trans;

	float offset;
	int current=0;


	// Use this for initialization
	void Start () {
		Canvas.ForceUpdateCanvases();
		instance = this;
		trans = GetComponent<RectTransform> ();
		trans.anchorMax = new Vector3 (weaponsUnits.Count, 1f);
		placeUnits ();
		RectTransform target = weaponsUnits [0].GetComponent<RectTransform>();
		offset = weaponsUnits [1].GetComponent<RectTransform> ().position.x - weaponsUnits [2].GetComponent<RectTransform> ().position.x;

	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (unitBehavior.Count);
	}

	public void placeUnits()
	{
		RectTransform unitTransform;
		UpgradeUnitBehavior b;
		int i = 0;
		float distance = 1f / weaponsUnits.Count;
		Debug.Log (weaponsUnits.Count);
		foreach(GameObject units in weaponsUnits)
		{
			unitTransform = units.GetComponent<RectTransform>();

			unitTransform.anchorMin = new Vector2(i * distance, 0.05f);
			unitTransform.anchorMax = new Vector2((i+1)*distance, 0.95f);
			i++;

			b = units.GetComponent<UpgradeUnitBehavior>();

		}
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
				Player.instance.addWeapon(units.associatedWeapon);
				Debug.Log (units.associatedWeapon);

			}
		}
	}

	public void SnapTo(int i)
	{

		current += i;

		Vector2 itemLoc = scrollRect.transform.InverseTransformPoint(weaponsUnits[current].transform.position);
		Vector2 target = scrollRect.transform.InverseTransformPoint (new Vector2(0f, weaponsUnits[current].transform.position.y));
		//Vector2 target = scrollRect.transform.InverseTransformPoint (scrollRect.GetComponent<RectTransform> ().TransformPoint (Vector2.zero));
		Vector2 diff = target - itemLoc;
		if (i > 0) {
			trans.anchoredPosition = trans.parent.InverseTransformPoint ((Vector2)trans.position + (diff * 1.2f));
		}
		else
		{
			trans.anchoredPosition = trans.parent.InverseTransformPoint ((Vector2)trans.position + (diff * 0.005f));
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
}
