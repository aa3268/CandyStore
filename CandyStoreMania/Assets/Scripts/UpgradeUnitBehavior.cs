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
	public int unlockCost;

	WeaponsInterface weaponBehavior;
	int[] upgradeLevel;

	// Use this for initialization
	void Start () {
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
		checkUpgradable ();
		LevelDirector.instance.purchase (unlockCost);
		Player.instance.addWeapon(associatedWeapon);
	}

	public void upgradeBaseDamage()
	{
		weaponBehavior.upgradeDamage ();
		LevelDirector.instance.purchase (upgradeLevel[0] * 50);
		baseDamage.value = weaponBehavior.getBaseDamage ();
		upgradeLevel [0] ++;
		checkUpgradable ();
	}

	public void upgradeCooldownTime()
	{
		weaponBehavior.upgradeFireRate ();
		LevelDirector.instance.purchase (upgradeLevel[1] * 50);
		cooldown.value = weaponBehavior.getCooldown ();
		upgradeLevel [1] ++;
		checkUpgradable ();
	}

	public void upgradeAmmo()
	{
		weaponBehavior.upgradeAmmo ();
		LevelDirector.instance.purchase (upgradeLevel[2] * 50);
		ammo.value = weaponBehavior.getAmmo ();
		upgradeLevel [2] ++;
		checkUpgradable ();
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
			statusText.text = "LOCKED " + unlockCost;
		}
	}

	public void checkUpgradable()
	{
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

		if (LevelDirector.instance.getAvailablePoints () >= (upgradeLevel[1] * 50) && weaponBehavior.getCooldown() < cooldown.maxValue && unlockStatus) {
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

		if (LevelDirector.instance.getAvailablePoints () >= (upgradeLevel[2] * 50) && weaponBehavior.getMaxAmmo() < ammo.maxValue && unlockStatus) {
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
			upgradeButtonA.SetActive (false);
			upgradeTextA.text = "Max Ammo";
			Avalue.text = "" + weaponBehavior.getAmmo();
			ammo.value = weaponBehavior.getAmmo ();
		}
	}
}
