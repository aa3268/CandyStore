using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WindowBehavior : MonoBehaviour {

	HealthBar healthBar;
	GameObject worldCanvas;
	List<EnemyScript> enemiesAtWindow;
	public int maxHealth;

	// Use this for initialization
	void Start () {
		worldCanvas = GameObject.Find ("HealthBarCanvas");
		Debug.Log (worldCanvas);
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
		}
	}

	public bool boardUp(int force)
	{
		healthBar.doDamage(force);
		
		LevelDirector.instance.updateHealth (-force);
		if (healthBar.isDepleted ()) 
		{
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
}
