using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

	bool claimed = false;
	int numClaimed;


	// Use this for initialization
	void Start () {
		numClaimed = 0;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Claim()
	{
		claimed = true;
		numClaimed++;
	}

	public bool getClaimed()
	{
		return claimed;
	}

	public int getClaimCount()
	{
		return numClaimed;
	}

	public void unClaim()
	{
		claimed = false;
		numClaimed--;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.name.Contains ("Enemy")) {

			EnemyScript e= other.gameObject.GetComponent<EnemyScript>();

			if(e.currentState.Equals(EnemyScript.States.EXIT))
			{
				other.gameObject.GetComponent<EnemyScript>().setTargetReached(true);
			}
		}
	}
}
