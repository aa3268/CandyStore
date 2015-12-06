using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeUnitBehavior : MonoBehaviour {
	
	public Text nameText;
	public Text infoText;
	public Text statusText;
	public Image weaponPic;
	public int associatedNum;

	public string weaponName;
	public string info;
	public string status;
	public Sprite picture;
	
	public bool unlockStatus = false;
	public GameObject unlockButton;
	public GameObject upgradeButtonBD;
	public GameObject upgradeButtonCS;
	public GameObject upgradeButtonA;
	
	public Text upgradeTextBD;
	public Text upgradeTextCS;
	public Text upgradeTextA;
	
	public Text BDvalue;
	public Text CSvalue;
	public Text Avalue;
	
	public Slider baseDamage;
	public Slider cooldown;
	public Slider ammo;
	
	public GameObject associatedWeapon;
	public double unlockCost;
	
	WeaponsInterface weaponBehavior;
	
	int[] upgradeLevel;
	double increment = 1.5;

	// Use this for initialization
	void Awake () {
		weaponBehavior = associatedWeapon.GetComponent<WeaponsInterface> ();
		nameText.text = name;
		infoText.text = info;
		statusText.text = status;
		weaponPic.sprite = picture;

		baseDamage.value = weaponBehavior.getBaseDamage ();
		cooldown.value = weaponBehavior.getCooldown ();
		ammo.value = weaponBehavior.getAmmo ();
		
		upgradeLevel = new int[3]{1,1,1};
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void unlock()
	{
		unlockStatus = true;
		unlockButton.SetActive (false);
		LevelDirector.instance.purchase (unlockCost);
		Player.instance.addWeapon(associatedWeapon, associatedNum);
		statusText.text = "UNLOCKED ";
		WeaponsUnit.instance.performChecks ();
	}
	
	public void upgradeBaseDamage()
	{
		weaponBehavior.upgradeDamage ();
		LevelDirector.instance.purchase (upgradeLevel[0] * increment);
		baseDamage.value = weaponBehavior.getBaseDamage ();
		upgradeLevel [0] ++;
		WeaponsUnit.instance.performChecks ();
	}
	
	public void upgradeCooldownTime()
	{
		weaponBehavior.upgradeFireRate ();
		LevelDirector.instance.purchase (upgradeLevel[1] * increment);
		cooldown.value = weaponBehavior.getCooldown ();
		upgradeLevel [1] ++;
		WeaponsUnit.instance.performChecks ();
	}
	
	public void upgradeAmmo()
	{
		weaponBehavior.upgradeAmmo ();
		LevelDirector.instance.purchase (upgradeLevel[2] * increment);
		ammo.value = weaponBehavior.getAmmo ();
		upgradeLevel [2] ++;
		WeaponsUnit.instance.performChecks ();
	}
	
	public bool getUnlockStatus()
	{
		return unlockStatus;
	}
	
	public void checkUnlockable()
	{
		if(LevelDirector.instance.getAvailablePoints () >= unlockCost && !unlockStatus)
		{
			unlockButton.SetActive(true);
			statusText.text = "LOCKED " + unlockCost.ToString("C");
			
		}
		else if(LevelDirector.instance.getAvailablePoints () < unlockCost && !unlockStatus)
		{
			unlockButton.SetActive(false);
			statusText.text = "LOCKED " + unlockCost.ToString("C");
		}
	}
	
	public void checkUpgradable()
	{
		if (LevelDirector.instance.getAvailablePoints () >= (upgradeLevel[0] * increment) && weaponBehavior.getBaseDamage() < baseDamage.maxValue 
		    && weaponBehavior.getBaseDamage() > 0 && unlockStatus) {
			upgradeButtonBD.SetActive (true);
			upgradeTextBD.text ="Base Damage: " + (upgradeLevel[0]*increment).ToString("C");
			BDvalue.text = weaponBehavior.getBaseDamage ().ToString("F2");
			baseDamage.value = weaponBehavior.getBaseDamage ();
			
			
		} else if (LevelDirector.instance.getAvailablePoints () < (upgradeLevel[0] * increment) && unlockStatus 
		           && weaponBehavior.getBaseDamage() < baseDamage.maxValue && weaponBehavior.getBaseDamage() > 0) {
			upgradeButtonBD.SetActive (false);
			upgradeTextBD.text = "Base Damage: " + (upgradeLevel[0]*increment).ToString("C");
			BDvalue.text = weaponBehavior.getBaseDamage ().ToString("F2");
			baseDamage.value = weaponBehavior.getBaseDamage ();
		} 
		else {
			upgradeButtonBD.SetActive (false);
			upgradeTextBD.text = "Base Damage";
			BDvalue.text = weaponBehavior.getBaseDamage ().ToString("F2");
			baseDamage.value = weaponBehavior.getBaseDamage ();

		}
		
		if (LevelDirector.instance.getAvailablePoints () >= (upgradeLevel[1] * increment) && weaponBehavior.getCooldown() > cooldown.minValue
		    && weaponBehavior.getCooldown() > 0 && unlockStatus) {
			upgradeButtonCS.SetActive (true);
			upgradeTextCS.text = "Cooldown Time: " + (upgradeLevel[1]*increment).ToString("C");
			CSvalue.text = weaponBehavior.getCooldown().ToString("F2");
			cooldown.value = (cooldown.maxValue - weaponBehavior.getCooldown ());
		} else if (LevelDirector.instance.getAvailablePoints () < (upgradeLevel[1] * increment) && unlockStatus && weaponBehavior.getCooldown() > cooldown.minValue
		    && weaponBehavior.getCooldown() > 0 ) {
			upgradeButtonCS.SetActive (false);
			upgradeTextCS.text = "Cooldown Time: " + (upgradeLevel[1]*increment).ToString("C");
			CSvalue.text = weaponBehavior.getCooldown().ToString("F2");
			cooldown.value = (cooldown.maxValue - weaponBehavior.getCooldown ());
		} else {
			upgradeButtonCS.SetActive (false);
			upgradeTextCS.text = "Cooldown Time";
			CSvalue.text = weaponBehavior.getCooldown().ToString("F2");
			cooldown.value = (cooldown.maxValue - weaponBehavior.getCooldown ());
		}
		
		if (LevelDirector.instance.getAvailablePoints () >= (upgradeLevel[2] * increment) && weaponBehavior.getMaxAmmo() < ammo.maxValue 
		    && weaponBehavior.getMaxAmmo() > 0 && unlockStatus) {
			upgradeButtonA.SetActive (true);
			upgradeTextA.text = "Max Ammo: " + (upgradeLevel[2]*increment).ToString("C");
			Avalue.text = "" + weaponBehavior.getMaxAmmo();
			ammo.value = weaponBehavior.getMaxAmmo ();
			
		} else if (LevelDirector.instance.getAvailablePoints () < (upgradeLevel[2] * increment) && unlockStatus  
		           && weaponBehavior.getMaxAmmo() < ammo.maxValue && weaponBehavior.getMaxAmmo() > 0) 
		{
			upgradeButtonA.SetActive (false);
			upgradeTextA.text = "Max Ammo: " + (upgradeLevel[2]*increment).ToString("C");
			Avalue.text = "" + weaponBehavior.getMaxAmmo();
			ammo.value = weaponBehavior.getMaxAmmo ();
		} else {
			upgradeButtonA.SetActive (false);
			upgradeTextA.text = "Max Ammo";
			Avalue.text = "" + weaponBehavior.getAmmo();
			ammo.value = weaponBehavior.getAmmo ();
		}
	}
}
