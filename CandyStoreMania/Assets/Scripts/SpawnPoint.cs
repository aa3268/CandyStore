using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

	float delay;
	float timePassed;
	bool claimed = false;
	int numClaimed;

	// Use this for initialization
	void Start () {
		delay = 5f;
		timePassed = 0f;
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
}
