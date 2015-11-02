using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyScript : MonoBehaviour {

	public float boardingSpeed;
	public int boardingForce;
	public int health;

	HealthBar healthBar;
	GameObject worldCanvas;

	float time;

	enum States {SEARCH, MOVE, DESTROY, EXIT};
	public enum SearchTypes { AID, FARTHEST, CLOSEST};

	NavMeshAgent nav;

	List<Transform> windowLocs;
	List<SpawnPoint>exitPoints;
	
	GameObject windows;

	WindowBehavior currentTarget;

	public int windowAggression;
	public int selfPreservasion;
	public int supportDestruction;

	States currentState;

	Director director;

	SearchTypes searchType;

	Vector3 exitPoint;


	// Use this for initialization
	void Awake () {
		nav = GetComponent<NavMeshAgent> ();
		windows = GameObject.Find ("WindowLocators");

		worldCanvas = GameObject.Find ("HealthBarCanvas");

		director = GameObject.Find ("EnemyDirector").GetComponent<Director> ();

		exitPoint = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {

		takeAction ();
		healthBar.setAttachedObjectPos (transform.position);
		if(healthBar.currentHealth <= 0)
		{
			
			healthBar.gameObject.SetActive (false);
			currentState = States.EXIT;
		}
	}


	void takeAction()
	{
		switch (currentState) 
		{
			case States.SEARCH:
				currentState = States.MOVE;
				nav.destination = searchWindow ();
				break;
			case States.MOVE:
				if(targetReached())
				{
					currentState = States.DESTROY;
				}
				break;
			case States.DESTROY:
				if(sealUp ())
				{
					currentState = States.SEARCH;
				}
				break;
			case States.EXIT:
				exit ();
				break;
		}
	}
	// go look for a window

	Vector3 searchWindow()
	{
		Debug.Log ("searching");
		Vector3 target = Vector3.zero;

		switch (searchType) 
		{
			case SearchTypes.CLOSEST:
				target = searchClosest ();
			break;
			case SearchTypes.FARTHEST:
				target = searchFurthest();
			break;
			case SearchTypes.AID:
				target = searchAid();
			break;
		}

		return target;
	}

	Vector3 searchClosest()
	{
		Vector3 closest = Vector3.zero;
		Vector3 closestWithoutOccupant = Vector3.zero;

		Vector3 currentPos = transform.position;
		Debug.Log ("am i here");
		bool first = true;
		bool firstUnoccupied = true;

		foreach (Transform t in windowLocs) 
		{
			if(first || firstUnoccupied)
			{
				if(first)
				{
					if(t.gameObject.GetComponent<WindowBehavior>().getEnemies().Count == 0)
					{
						closestWithoutOccupant = t.position;
						currentTarget = t.gameObject.GetComponent<WindowBehavior>();
						firstUnoccupied = false;
					}
					else
					{
						closest = t.position;
						currentTarget = t.gameObject.GetComponent<WindowBehavior>();
					}
					first = false;
				}
				else if(t.gameObject.GetComponent<WindowBehavior>().getEnemies().Count == 0)
				{
					closestWithoutOccupant = t.position;
					currentTarget = t.gameObject.GetComponent<WindowBehavior>();
					firstUnoccupied = false;
				}
			}
			else if(firstUnoccupied && Vector3.Distance(currentPos, t.position) < Vector3.Distance(currentPos, closest))
			{
				currentTarget = t.gameObject.GetComponent<WindowBehavior>();
				closest = t.position;
			}
			else if(!firstUnoccupied && (t.GetComponent<WindowBehavior>().getEnemies().Count == 0 ) 
			        && Vector3.Distance(currentPos, t.position) < Vector3.Distance(currentPos, closest))
			{
				closestWithoutOccupant = t.position;
				currentTarget = t.gameObject.GetComponent<WindowBehavior>();
			}
		}

		if(closest.Equals(Vector3.zero) && closestWithoutOccupant.Equals (Vector3.zero))
		{
			currentState = States.EXIT;
		}

		if(!closestWithoutOccupant.Equals (Vector3.zero))
		{
			return closestWithoutOccupant;
		}

		currentTarget.getEnemies().Add(this);

		return closest;
	}


	Vector3 searchFurthest()
	{
		Vector3 furthest = Vector3.zero;
		Vector3 furthestWithoutOccupant = Vector3.zero;
		
		Vector3 currentPos = transform.position;
		bool first = true;
		bool firstUnoccupied = true;
		
		foreach (Transform t in windowLocs) 
		{
			if(first || firstUnoccupied)
			{
				if(first)
				{
					furthest = t.position;
					currentTarget = t.gameObject.GetComponent<WindowBehavior>();
					first = false;
				}
				else if(t.gameObject.GetComponent<WindowBehavior>().getEnemies().Count == 0)
				{
					furthestWithoutOccupant = t.position;
					currentTarget = t.gameObject.GetComponent<WindowBehavior>();
					firstUnoccupied = false;
				}
			}
			else if(firstUnoccupied && Vector3.Distance(currentPos, t.position) > Vector3.Distance(currentPos, furthest))
			{
				currentTarget = t.gameObject.GetComponent<WindowBehavior>();
				furthest = t.position;
			}
			else if(!firstUnoccupied && (t.GetComponent<WindowBehavior>().getEnemies().Count == 0 ) 
			        && Vector3.Distance(currentPos, t.position) > Vector3.Distance(currentPos, furthest))
			{
				furthestWithoutOccupant = t.position;
				currentTarget = t.gameObject.GetComponent<WindowBehavior>();
			}
		}
		
		if(furthest.Equals(Vector3.zero) && furthestWithoutOccupant.Equals(Vector3.zero))
		{
			currentState = States.EXIT;
		}

		if (furthestWithoutOccupant.Equals (Vector3.zero)) 
		{
			return furthestWithoutOccupant;
		}

		currentTarget.getEnemies().Add(this);
		
		return furthest;
	}

	Vector3 searchAid()
	{
		Vector3 leastPeople = Vector3.zero;
		
		Vector3 currentPos = transform.position;
		int currentPeopleCount = 0;
		bool first = true;
		foreach (Transform t in windowLocs) 
		{
			if(first)
			{
				if(t.gameObject.GetComponent<WindowBehavior>().getEnemies().Count > 0)
				{
					leastPeople = t.position;
					currentTarget = t.gameObject.GetComponent<WindowBehavior>();
					currentPeopleCount = t.gameObject.GetComponent<WindowBehavior>().getEnemies().Count;
					first = false;
				}
			}
			else 
			{
				if(t.gameObject.GetComponent<WindowBehavior>().getEnemies().Count < currentPeopleCount)
				{
					leastPeople = t.position;
					currentTarget = t.gameObject.GetComponent<WindowBehavior>();
					currentPeopleCount = t.gameObject.GetComponent<WindowBehavior>().getEnemies().Count;
				}
			}
		}

		if(leastPeople.Equals(Vector3.zero))
		{
			leastPeople = searchClosest();
		}

		currentTarget.getEnemies().Add(this);
		return leastPeople;
	}



	/*
	 * Get the hell outta dodge
	 * 
	 Vector3 dodge()
	{

	}*/


	// board up windows
	
	bool sealUp()
	{
		if(time > boardingSpeed)
		{
			if(currentTarget != null)
			{
				if (currentTarget.boardUp (boardingForce))
				{
					//currentTarget.markBoarded();
					windowLocs.Remove(currentTarget.transform);
					return true;
				}
			}
			else
			{
				return true;
			}

			time = 0f;
		}

		time += Time.deltaTime;

		return false;
	}

	bool targetReached()
	{
		Vector3 targetDestination = new Vector3 (nav.destination.x, 0f, nav.destination.z);
		Vector3 currentPosition = new Vector3 (transform.position.x, 0f, transform.position.z);

		if(Vector3.Distance(targetDestination, currentPosition) < 1f)
		{
			nav.destination = currentPosition;
			return true;
		}
		return false;
	}

	// search for candy refill sources

	// destroy candy refill sources

	// leave the store behavior

	// various health scripts -- extension of health script on candy vats and windows!

	// director to direct flow and deployment of parents


	void exit()
	{
		if(exitPoint.Equals(Vector3.zero))
		{
			nav.destination = SearchExitPoint();
		}

		Vector3 targetDestination = new Vector3 (nav.destination.x, 0f, nav.destination.z);
		Vector3 currentPosition = new Vector3 (transform.position.x, 0f, transform.position.z);

		if(Vector3.Distance(targetDestination, currentPosition) < 1f)
		{
			currentTarget.getEnemies().Remove(this);
			director.removeEnemy(this);
		}
	}

	public void createHealthBar()
	{
		if (worldCanvas == null) 
		{
			worldCanvas = GameObject.Find ("HealthBarCanvas");
		}
		GameObject bar = (GameObject)Instantiate (Resources.Load ("prefabs/healthBar"));
		healthBar = bar.GetComponent<HealthBar> ();
		healthBar.setColor (Color.red);
		healthBar.transform.SetParent (worldCanvas.transform, false);
		healthBar.setAttachedObjectPos (transform.position);
		healthBar.gameObject.SetActive (false);
	
	}

	public HealthBar getHealthBar()
	{
		return healthBar;
	}

	public void readyEnemy()
	{
		windowLocs = new List<Transform> ();
		
		foreach (Transform t in windows.transform) 
		{
			windowLocs.Add (t);
		}
		
		currentState = States.SEARCH;

		healthBar.reFillHealth ();
		healthBar.setAttachedObjectPos (transform.position);
		healthBar.gameObject.SetActive (true);
		time = 0f;
	}

	public NavMeshAgent getAgent()
	{
		return nav;
	}

	public void setSearchType(SearchTypes type)
	{
		searchType = type;
	}


	void OnCollisionEnter(Collision bullet)
	{
		if (bullet.gameObject.name.Contains("Bullet")) {
			healthBar.doDamage(20);

		}
	}

	public Vector3 SearchExitPoint()
	{
		Vector3 exit = Vector3.zero;

		if(exitPoints != null)
		{
			exitPoints = Director.instance.getSpawnPoints();
		}

		for(int i = 0; i < exitPoints.Count; i++)
		{
			if(!exitPoints[i].getClaimed())
			{
				if(exit.Equals(Vector3.zero))
				{
					exit = exitPoints[i].transform.position;
				}
				else
				{
					if(Vector3.Distance(transform.position, exitPoints[i].transform.position) < Vector3.Distance(transform.position, exit))
					{
						exit = exitPoints[i].transform.position;
					}
				}
			}
		}
		
		if(exit.Equals (Vector3.zero))
		{
			for(int i = 0; i < exitPoints.Count; i++)
			{
				if(!exitPoints[i].getClaimed())
				{
					if(Vector3.Distance(transform.position, exitPoints[i].transform.position) < Vector3.Distance(transform.position, exit))
					{
						exit = exitPoints[i].transform.position;
					}
				}
			}
		}

		return exit;
	}

	public void setExits(List<SpawnPoint> points)
	{
		exitPoints = points;
	}

}
