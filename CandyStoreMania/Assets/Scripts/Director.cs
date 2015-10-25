using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Director : MonoBehaviour {

	enum State { FREE, USED };
	enum EnemyType { AGGRESSIVE, SUPPORTER, RUNNER, VANGUARD };

	private List<GameObject> enemyPool;
	private List<GameObject> enemyInUse;
	private GameObject pointsOfEntry;

	private int numAvailable;

	public int totalEnemies;
	public int waveSize;
	public float timeDelay;

	float time = 0f;
	bool poolReady = false;

	private int enemiesCreated = 0;


	// Use this for initialization
	void Start () {
		enemyPool = new List<GameObject> ();
		enemyInUse = new List<GameObject> ();

		pointsOfEntry = GameObject.Find ("Door");
		createPool ();
	}
	
	// Update is called once per frame
	void Update () {

		if (poolReady && (enemiesCreated < totalEnemies)) {
			placeEnemy ();
		}
	}

	public void createPool()
	{
		for(int i = 0; i < waveSize; i++)
		{
			GameObject e = (GameObject)Instantiate (Resources.Load ("prefabs/enemy"));
			e.GetComponent<EnemyScript> ().createHealthBar ();

			e.SetActive (false);

			e.transform.SetParent(gameObject.transform);
			enemyPool.Add (e);

			numAvailable++;
		}

		poolReady = true;
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
					enemyInUse.Add (enemyPool[enemyPool.Count - 1]);
					readyEnemy (enemyPool[enemyPool.Count - 1]);
					numAvailable--;
					enemyPool.RemoveAt(enemyPool.Count -1);
					enemiesCreated++;

				}

				time = 0f;
			}

			time += Time.deltaTime;
		}
	}

	void readyEnemy(GameObject e)
	{
		e.SetActive (true);
		e.transform.position = pointsOfEntry.transform.position;
		e.GetComponent<EnemyScript> ().readyEnemy ();

		int type = Random.Range (0, 4);

		switch (type) {
			case 0:
				setEnemyBehavior (e.GetComponent<EnemyScript> (), EnemyType.AGGRESSIVE);
				break;
			case 1:
				setEnemyBehavior (e.GetComponent<EnemyScript> (), EnemyType.RUNNER);
				break;
			case 2:
				setEnemyBehavior (e.GetComponent<EnemyScript> (), EnemyType.VANGUARD);
				break;
			case 3:
				setEnemyBehavior (e.GetComponent<EnemyScript> (), EnemyType.SUPPORTER);
				break;
		}

	}

	void setEnemyBehavior(EnemyScript e, EnemyType type)
	{
		Debug.Log (type);
		switch(type)
		{
			case EnemyType.AGGRESSIVE:
				e.boardingForce = 6;
				e.boardingSpeed = 0.3f;
				e.getAgent().speed = 2f;
				e.getAgent().radius = 1.5f;
				e.health = 75;	
				e.setSearchType(EnemyScript.SearchTypes.CLOSEST);
			break;
			case EnemyType.RUNNER:
				e.boardingForce = 2;
				e.boardingSpeed = 0.1f;
				e.getAgent().speed = 4f;
				e.getAgent().radius = 0.5f;
				e.health = 100;
				e.setSearchType(EnemyScript.SearchTypes.CLOSEST);
			break;
			case EnemyType.VANGUARD:
				e.boardingForce = 3;
				e.boardingSpeed = 0.15f;
				e.getAgent().speed = 3f;
				e.getAgent().radius = 0.75f;
				e.health = 70;
				e.setSearchType(EnemyScript.SearchTypes.FARTHEST);
			break;

			case EnemyType.SUPPORTER:
				e.boardingForce = 1;
				e.boardingSpeed = 0.1f;
				e.getAgent().speed = 3f;
				e.getAgent().radius = 0.75f;
				e.health = 125;
				e.setSearchType(EnemyScript.SearchTypes.AID);
			break;
		}
	}

	public void removeEnemy(EnemyScript e)
	{
		e.getHealthBar ().enabled = false;
		e.gameObject.SetActive (false);

		enemyPool.Add (e.gameObject);
		enemyInUse.Remove (e.gameObject);

		numAvailable ++;
	}
}
