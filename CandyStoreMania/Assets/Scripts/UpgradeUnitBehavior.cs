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
		checkUpgradable ();
		LevelDirector.instance.purchase (unlockCost);
		Player.instance.addWeapon(associatedWeapon);
	}

	public void upgrade()
	{
		weaponBehavior.upgrade ();
		LevelDirector.instance.purchase (weaponBehavior.upgradeCost());
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
		} else {
			infoText.text = "Base Damage: " + weaponBehavior.getBaseDamage () + "  Cooldown: " + weaponBehavior.getCooldown () + "  Max Shots: " + 
				weaponBehavior.getMaxAmmo ();
		}
	}
}
