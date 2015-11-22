using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelDirector : MonoBehaviour {

	public static LevelDirector instance;
	public Slider totalHealth;
	public Director enemyDirector;
	public GameObject windLocs;
	public AnimationCurve waveSizePerLevel;
	public AnimationCurve maxEnemiesPerLevel;
	public int maxLevels;
	public int maxWaveSize;
	public int maxEnemies;

	List<WindowBehavior> windows;
	List<Transform> targetLocs;
	GameObject targets;
	int currentLevel;
	int maxHealth;
	int currentHealth;
	GameObject player;

	int totalScore;
	int available;

	// Use this for initialization
	void Start () {
		instance = this;
		currentLevel = 0;
		maxHealth = 0;
		currentHealth = 0;
		totalScore = 0;
		available = 0;

		windows = new List<WindowBehavior> ();
		player = GameObject.Find ("Player");
		targets = GameObject.Find ("TargetLocators");
		setUpLevel ();
	}
	
	// Update is called once per frame
	void Update () {
		totalHealth.maxValue = maxHealth;
		totalHealth.value = currentHealth;

		checkGameOver ();
	}

	public void setUpLevel()
	{
		Player.instance.enabled = true;
		targetLocs = new List<Transform> ();
		
		foreach (Transform t in targets.transform) 
		{
			targetLocs.Add (t);
		}

		currentLevel++;
		totalHealth.gameObject.SetActive (true);

		if (currentLevel == 1) {

			foreach (Transform t in windLocs.transform) {
				windows.Add (t.GetComponent<WindowBehavior> ());
				windows [windows.Count - 1].createHealthBar ();
			}
		}
		
		for(int i =  0; i < windows.Count; i++)
		{
			windows[i].refreshHealth();
		}
	

		enemyDirector.waveSize = (int) (waveSizePerLevel.Evaluate (((float)currentLevel) / maxLevels) * maxWaveSize);
		enemyDirector.totalEnemies = (int) (maxEnemiesPerLevel.Evaluate(((float)currentLevel)/maxLevels) * maxEnemies);

		if(enemyDirector.waveSize > enemyDirector.totalEnemies)
		{
			enemyDirector.waveSize = enemyDirector.totalEnemies;
		}
		totalHealth.maxValue = maxHealth;
		currentHealth = maxHealth;
		totalHealth.value = currentHealth;

		if(currentLevel > 1)
		{
			enemyDirector.reset();
			enemyDirector.createPool ();
		}
	}

	public int getLevel()
	{
		return currentLevel;
	}

	public void addMaxHealth(int max)
	{
		maxHealth += max;
		currentHealth = maxHealth;
	}

	public void updateHealth(int health)
	{
		currentHealth += health;
	}

	public void checkGameOver()
	{
		if(currentHealth <= 0)
		{
			Application.LoadLevel ("GameOver");
		}
	}

	public void levelOver()
	{
		totalScore += (currentHealth * currentLevel);
		available += (currentHealth * currentLevel);
		WeaponsUnit.instance.performChecks ();
		UpgradeMenu.instance.ScaleUp();
		Time.timeScale = 0.00000000001f;
		Player.instance.paused = true;
		Player.instance.enabled = false;
		totalHealth.gameObject.SetActive (false);
	}

	public int getScore()
	{
		return totalScore;
	}

	public int getAvailablePoints()
	{
		return available;
	}

	public void purchase(int cost)
	{
		available -= cost;
		UpgradeMenu.instance.available.text = "Points available: " + LevelDirector.instance.getAvailablePoints ();
	}

	public List<Transform> getTargets()
	{
		return targetLocs;
	}

	public void removeTarget(Transform t)
	{
		targetLocs.Remove (t);

		for(int i = 0; i < Director.instance.getEnemiesInUse().Count; i++)
		{
			EnemyScript e = Director.instance.getEnemiesInUse()[i].GetComponent<EnemyScript>();
			e.checkTargetValid(t);
		}
	}
}
