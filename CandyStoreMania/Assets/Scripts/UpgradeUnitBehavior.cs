using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeUnitBehavior : MonoBehaviour {

	public Text nameText;
	public Text infoText;
	public Text statusText;
	public Image weaponPic;

	public string weaponName;
	public string info;
	public string status;
	public Sprite picture;

	public bool unlockStatus = false;
	public GameObject unlockButton;
	public GameObject upgradeButton;
	public GameObject associatedWeapon;
	public int unlockCost;

	WeaponsInterface weaponBehavior;

	int[] upgradeLevel;

	// Use this for initialization
	void Start () {
		weaponBehavior = associatedWeapon.GetComponent<WeaponsInterface> ();
		nameText.text = name;
		infoText.text = "Base Damage: " + weaponBehavior.getBaseDamage () + "  Cooldown: " + weaponBehavior.getCooldown () + "  Max Shots: " + 
			weaponBehavior.getAmmo ();
		statusText.text = status;
		weaponPic.sprite = picture;
		
		if(unlockStatus)
		{
			unlockButton.SetActive(false);
			upgradeButton.SetActive(true);
		}
		else
		{
			unlockButton.SetActive(false);
			upgradeButton.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void unlock()
	{
		unlockStatus = true;
		unlockButton.SetActive (false);
		WeaponsUnit.instance.performChecks ();
		LevelDirector.instance.purchase (unlockCost);
		Player.instance.addWeapon(associatedWeapon);
	}

	public void upgrade()
	{
		weaponBehavior.upgrade ();
		LevelDirector.instance.purchase (weaponBehavior.upgradeCost());
		checkUpgradable ();
		weaponBehavior.upgradeDamage ();
		LevelDirector.instance.purchase (upgradeLevel[0] * 50);
		baseDamage.value = weaponBehavior.getBaseDamage ();
		upgradeLevel [0] ++;
		WeaponsUnit.instance.performChecks ();
	}

	public void upgradeCooldownTime()
	{
		weaponBehavior.upgradeFireRate ();
		LevelDirector.instance.purchase (upgradeLevel[1] * 50);
		cooldown.value = weaponBehavior.getCooldown ();
		upgradeLevel [1] ++;
		WeaponsUnit.instance.performChecks ();
	}

	public void upgradeAmmo()
	{
		weaponBehavior.upgradeAmmo ();
		LevelDirector.instance.purchase (upgradeLevel[2] * 50);
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
			statusText.text = "LOCKED " + unlockCost;

		}
		else if(LevelDirector.instance.getAvailablePoints () < unlockCost && !unlockStatus)
		{
			unlockButton.SetActive(false);
			statusText.text = "LOCKED " + unlockCost;
		}
	}

	public void checkUpgradable()
	{
		if (LevelDirector.instance.getAvailablePoints () >= weaponBehavior.upgradeCost () && unlockStatus) {
			upgradeButton.SetActive (true);
			statusText.text = "UPGRADE COST: " + weaponBehavior.upgradeCost ();
			infoText.text = "Base Damage: " + weaponBehavior.getBaseDamage () + "  Cooldown: " + weaponBehavior.getCooldown () + "  Max Shots: " + 
				weaponBehavior.getMaxAmmo ();
		} else if (LevelDirector.instance.getAvailablePoints () < weaponBehavior.upgradeCost () && unlockStatus) {
			upgradeButton.SetActive (false);
			statusText.text = "UPGRADE COST: " + weaponBehavior.upgradeCost ();
			infoText.text = "Base Damage: " + weaponBehavior.getBaseDamage () + "  Cooldown: " + weaponBehavior.getCooldown () + "  Max Shots: " + 
				weaponBehavior.getMaxAmmo ();
		}
		if (LevelDirector.instance.getAvailablePoints () >= (upgradeLevel[0] * 50) && weaponBehavior.getBaseDamage() < baseDamage.maxValue 
		    && weaponBehavior.getBaseDamage() > 0 && unlockStatus) {
			upgradeButtonBD.SetActive (true);
			upgradeTextBD.text ="Base Damage: " + (upgradeLevel[0]*50);
			BDvalue.text = "" + weaponBehavior.getBaseDamage ();
			baseDamage.value = weaponBehavior.getBaseDamage ();
	

		} else if (LevelDirector.instance.getAvailablePoints () < (upgradeLevel[0] * 50) && unlockStatus) {
			upgradeButtonBD.SetActive (false);
			upgradeTextBD.text = "Base Damage: " + (upgradeLevel[0]*50);
			BDvalue.text = "" + weaponBehavior.getBaseDamage ();
			baseDamage.value = weaponBehavior.getBaseDamage ();
		} 
		else {
			upgradeButtonBD.SetActive (false);
			upgradeTextBD.text = "Base Damage";
			BDvalue.text = "" + weaponBehavior.getBaseDamage ();
			baseDamage.value = weaponBehavior.getBaseDamage ();
		}

		if (LevelDirector.instance.getAvailablePoints () >= (upgradeLevel[1] * 50) && weaponBehavior.getCooldown() > cooldown.minValue
		    && weaponBehavior.getCooldown() > 0 && unlockStatus) {
			upgradeButtonCS.SetActive (true);
			upgradeTextCS.text = "Cooldown Time: " + (upgradeLevel[1]*50);
			CSvalue.text = "" + weaponBehavior.getCooldown();
			cooldown.value = (cooldown.maxValue - weaponBehavior.getCooldown ());
		} else if (LevelDirector.instance.getAvailablePoints () < (upgradeLevel[1] * 50) && unlockStatus) {
			upgradeButtonCS.SetActive (false);
			upgradeTextCS.text = "Cooldown Time: " + (upgradeLevel[1]*50);
			CSvalue.text = "" + weaponBehavior.getCooldown();
			cooldown.value = (cooldown.maxValue - weaponBehavior.getCooldown ());
		} else {
			upgradeButtonCS.SetActive (false);
			upgradeTextCS.text = "Cooldown Time";
			CSvalue.text = "" + (cooldown.maxValue - weaponBehavior.getCooldown());
			cooldown.value = (cooldown.maxValue - weaponBehavior.getCooldown ());
		}

		if (LevelDirector.instance.getAvailablePoints () >= (upgradeLevel[2] * 50) && weaponBehavior.getMaxAmmo() < ammo.maxValue 
		    && weaponBehavior.getMaxAmmo() > 0 && unlockStatus) {
			upgradeButtonA.SetActive (true);
			upgradeTextA.text = "Max Ammo: " + (upgradeLevel[2]*50);
			Avalue.text = "" + weaponBehavior.getMaxAmmo();
			ammo.value = weaponBehavior.getMaxAmmo ();

		} else if (LevelDirector.instance.getAvailablePoints () < (upgradeLevel[2] * 50) && unlockStatus) {
			upgradeButtonA.SetActive (false);
			upgradeTextA.text = "Max Ammo: " + (upgradeLevel[2]*50);
			Avalue.text = "" + weaponBehavior.getMaxAmmo();
			ammo.value = weaponBehavior.getMaxAmmo ();
		} else {
			infoText.text = "Base Damage: " + weaponBehavior.getBaseDamage () + "  Cooldown: " + weaponBehavior.getCooldown () + "  Max Shots: " + 
				weaponBehavior.getMaxAmmo ();
		}
	}
}
