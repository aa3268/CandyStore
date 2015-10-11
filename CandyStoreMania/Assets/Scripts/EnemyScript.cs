using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyScript : MonoBehaviour {

	NavMeshAgent nav;
	List<Transform> windowLocs;
	GameObject windows;

	bool hasTarget;

	public int windowAggression;
	public int selfPreserversion;

	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent> ();
		windows = GameObject.Find ("WindowLocators");
		windowLocs = new List<Transform> ();
		foreach (Transform t in windows.transform) 
		{
			windowLocs.Add(t);
		}

		hasTarget = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasTarget)
		{
			nav.destination = searchWindow ();
		}
	}

	Vector3 searchWindow()
	{
		Vector3 closest = Vector3.zero;
		Vector3 currentPos = transform.position;
		bool first = true;

		foreach (Transform t in windows.transform) 
		{
			if(first)
			{
				closest = t.position;
				first = false;
			}
			else if(Vector3.Distance(currentPos, t.position) < Vector3.Distance(currentPos, closest))
			{
				closest = t.position;
			}
		}

		hasTarget = true;
		return closest;
	}

	Vector3 dodge()
	{

	}

	void sealUp()
	{

	}

}
