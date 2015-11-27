using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Director : MonoBehaviour {

	public static Director instance;
	public GameObject doorLocator;
	enum State { FREE, USED };
	enum EnemyType { AGGRESSIVE, SUPPORTER, RUNNER, VANGUARD };

	private List<GameObject> enemyPool;
	private List<GameObject> enemyInUse;
	
	List<SpawnPoint> spawnPoints;

	private int numAvailable;

	public int totalEnemies;
	public int waveSize;
	public float timeDelay;

	float time = 0f;
	bool poolReady = false;
	bool overStarted = false;

	private int enemiesDestroyed;
	private int enemiesCreated;

	// Use this for initialization
	void Start () {
		enemyPool = new List<GameObject> ();
		enemyInUse = new List<GameObject> ();

		spawnPoints = new List<SpawnPoint>();
		
		foreach(Transform t in doorLocator.transform)
		{
			spawnPoints.Add(t.gameObject.GetComponent<SpawnPoint>());
		}

		createPool ();
		instance = this;
		enemiesDestroyed = 0;
		enemiesCreated = 0;
	}

	// Update is called once per frame
	void Update () {

		if (poolReady && (enemiesCreated < totalEnemies)) {
			placeEnemy ();
		}

		if(levelOver() && !overStarted)
		{
			LevelDirector.instance.levelOver();

			overStarted = true;
		}
	}

	public void createPool()
	{
		for(int i = 0; i < waveSize; i++)
		{	
			Debug.Log ("Create " + LevelDirector.instance.getLevel() + " " + waveSize);
			GameObject e = null;
			int type = Random.Range(0, 4);
		
			switch (type) {
			case 0:
				e = (GameObject)Instantiate (Resources.Load ("prefabs/AggressiveEnemy"));
				setEnemyBehavior (e.GetComponent<EnemyScript>(), EnemyType.AGGRESSIVE);
				break;
			case 1:
				e = (GameObject)Instantiate (Resources.Load ("prefabs/RunnerEnemy"));
				setEnemyBehavior (e.GetComponent<EnemyScript>(), EnemyType.RUNNER);
				break;
			case 2:
				e = (GameObject)Instantiate (Resources.Load ("prefabs/VanguardEnemy"));
				setEnemyBehavior (e.GetComponent<EnemyScript>(), EnemyType.VANGUARD);
				break;
			case 3:
				e = (GameObject)Instantiate (Resources.Load ("prefabs/HelperEnemy"));
				setEnemyBehavior (e.GetComponent<EnemyScript>(), EnemyType.SUPPORTER);
				break;
			}

			e.GetComponent<EnemyScript> ().createHealthBar ();
			e.GetComponent<EnemyScript>().setExits(spawnPoints);
			e.SetActive (false);

			e.transform.SetParent(gameObject.transform);
			enemyPool.Add (e);

			numAvailable++;
		}

		poolReady = true;
	}

	public void reset()
	{

		foreach(GameObject e in enemyPool)
		{
			GameObject.Destroy(e);

		}
		enemyPool.Clear ();
		numAvailable = 0;
		enemiesCreated = 0;
		overStarted = false;
		enemiesDestroyed = 0;
	}

	public void placeEnemy()
	{
		int choice;
		if(numAvailable > 0)
		{
			if(time >= timeDelay)
			{
				choice = Random.Range (0, 2);
				if (choice == 0) 
				{	
					GameObject en = enemyPool[0];
					enemyInUse.Add (en);
					readyEnemy (en);
					numAvailable--;
					enemyPool.Remove(en);
					enemiesCreated++;
				}

				time = 0f;
			}

			time += Time.deltaTime;
		}


	}

	void readyEnemy(GameObject e)
	{
		int point = Random.Range (0, spawnPoints.Count);
		e.transform.position = spawnPoints [point].transform.position;
		EnemyScript enemy = e.GetComponent<EnemyScript> ();

		e.SetActive (true);

		int type = Random.Range (0, 4);

		enemy.readyEnemy ();

	}

	void setEnemyBehavior(EnemyScript e, EnemyType type)
	{
		switch(type)
		{
			case EnemyType.AGGRESSIVE:
				e.boardingForce = 4;
				e.boardingSpeed = 0.3f;
				e.getAgent().speed = 2f;
				e.health = 100;	
				e.windowAggression = 6;
				e.setSearchType(EnemyScript.SearchTypes.CLOSEST);
			break;
			case EnemyType.RUNNER:
				e.boardingForce = 2;
				e.boardingSpeed = 0.1f;
				e.getAgent().speed = 4f;
				e.health = 90;
				e.windowAggression = 1;
				e.setSearchType(EnemyScript.SearchTypes.CLOSEST);
			break;
			case EnemyType.VANGUARD:
				e.boardingForce = 2;
				e.boardingSpeed = 0.15f;
				e.getAgent().speed = 3f;
				e.health = 70;
				e.windowAggression = 4;
				e.setSearchType(EnemyScript.SearchTypes.FARTHEST);
			break;

			case EnemyType.SUPPORTER:
				e.boardingForce = 1;
				e.boardingSpeed = 0.1f;
				e.getAgent().speed = 3f;
				e.health = 120;
				e.windowAggression = 7;
				e.setSearchType(EnemyScript.SearchTypes.AID);
			break;
		}
	}

	public void removeEnemy(EnemyScript e)
	{
		e.gameObject.SetActive (false);
		enemyPool.Add (e.gameObject);
		enemyInUse.Remove (e.gameObject);

		numAvailable ++;
	}

	bool levelOver()
	{
		if(enemiesDestroyed == totalEnemies)
		{
			return true;
		}

		return false;
	}

	
	public List<SpawnPoint> getSpawnPoints()
	{
		return spawnPoints;
	}

	public List<GameObject> getEnemiesInUse()
	{
		return enemyInUse;
	}

	public void enemyDestroyed()
	{
		enemiesDestroyed++;
	}
}
