using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyScript : MonoBehaviour {

	enum States {SEARCH, MOVE, DESTROY, EXIT};
	NavMeshAgent nav;
	List<Transform> windowLocs;
	GameObject windows;
	GameObject door;

	GameObject currentTarget;

	public int windowAggression;
	public int selfPreservasion;
	public int supportDestruction;

	States currentState;

	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent> ();
		windows = GameObject.Find ("WindowLocators");
		door = GameObject.Find ("Door");

		windowLocs = new List<Transform> ();

		foreach (Transform t in windows.transform) 
		{
			windowLocs.Add(t);
		}

		currentState = States.SEARCH;
	}
	
	// Update is called once per frame
	void Update () {

		takeAction ();
	}


	void takeAction()
	{
		switch (currentState) 
		{
			case States.SEARCH:
				currentState = States.MOVE;
				nav.destination = searchWindow ();
				Debug.Log (nav.destination + " " + searchWindow());
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
		Vector3 closest = Vector3.zero;
		Vector3 currentPos = transform.position;
		bool first = true;

		foreach (Transform t in windows.transform) 
		{
			Debug.Log (closest);
			if(first)
			{
				closest = t.position;
				currentTarget = t.gameObject;
				first = false;
			}
			else if(Vector3.Distance(currentPos, t.position) < Vector3.Distance(currentPos, closest))
			{
				currentTarget = t.gameObject;
				closest = t.position;
			}
		}

		if(closest.Equals(Vector3.zero))
		{
			currentState = States.EXIT;
		}
		return closest;
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
		GameObject.Destroy (currentTarget);
		return true;
	}

	bool targetReached()
	{
		Vector3 targetDestination = new Vector3 (nav.destination.x, 0f, nav.destination.z);
		Vector3 currentPosition = new Vector3 (transform.position.x, 0f, transform.position.z);

		if(Vector3.Distance(targetDestination, currentPosition) < 1f)
		{
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
		if(!nav.destination.Equals(door.transform.position))
		{
			nav.destination = door.transform.position;
		}
		Vector3 targetDestination = new Vector3 (nav.destination.x, 0f, nav.destination.z);
		Vector3 currentPosition = new Vector3 (transform.position.x, 0f, transform.position.z);

		if(Vector3.Distance(targetDestination, currentPosition) < 0.05f)
		{
			Debug.Log ("DESTROY!!");
			GameObject.Destroy(gameObject);
		}
	}

}
