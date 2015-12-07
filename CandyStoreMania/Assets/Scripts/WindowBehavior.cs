using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WindowBehavior : MonoBehaviour {

	HealthBar healthBar;
	GameObject worldCanvas;
	public GameObject firstStage;
	public GameObject secondStage;

	List<EnemyScript> enemiesAtWindow;
	public int maxHealth;
	public GameObject associatedObject;
	protected int currentStage = 0;

	// Use this for initialization
	void Start () {
		worldCanvas = GameObject.Find ("HealthBarCanvas");
		enemiesAtWindow = new List<EnemyScript> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void createHealthBar()
	{
		if (worldCanvas == null) {
			worldCanvas = GameObject.Find ("HealthBarCanvas");
		}
		GameObject bar = (GameObject)Instantiate (Resources.Load ("prefabs/healthBar"));
		healthBar = bar.GetComponent<HealthBar> ();
		healthBar.setMaxHealth (maxHealth);
		healthBar.transform.SetParent (worldCanvas.transform, false);
		healthBar.setAttachedObjectPos (transform.position);
		LevelDirector.instance.addMaxHealth (maxHealth);
	}

	public void refreshHealth()
	{
		if (healthBar != null) {
			healthBar.reFillHealth ();
			healthBar.showHealthbar(true);
		}

		reset ();
	}

	public bool boardUp(int force)
	{
		healthBar.doWideEffectDamage(force);
	
		if (currentStage == 0 && healthBar.currentHealth <= healthBar.getMaxHealth () / 2f) 
		{
			changeStage();
		}

		if (healthBar.isDepleted ()) 
		{
			healthBar.showHealthbar(false);
			changeStage();
			return true;
		}
		return false;
	}

	public void markBoarded()
	{
		GameObject.Destroy (healthBar.gameObject);
		GameObject.Destroy (this.gameObject);
	}

	public void addEnemy(EnemyScript e)
	{
		enemiesAtWindow.Add (e);
	}

	public List<EnemyScript> getEnemies()
	{
		return enemiesAtWindow;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.name.Contains ("Enemy") && other.gameObject.GetComponent<EnemyScript>().getCurrentTarget().Equals(this)) {

			other.gameObject.GetComponent<EnemyScript>().setTargetReached(true);
		}
	}

	public virtual void changeStage()
	{
		if(currentStage < 2)
		{
			currentStage++;
		}
		
		if(currentStage == 1)
		{
			firstStage.SetActive(true);
		}
		else if(currentStage == 2)
		{
			secondStage.SetActive(true);
		}
	}
	
	public virtual void reset()
	{
		currentStage = 0;
		secondStage.SetActive (false);
		firstStage.SetActive (false);
	}

}
