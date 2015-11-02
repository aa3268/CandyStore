using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelDirector : MonoBehaviour {

	public static LevelDirector instance;
	public Director enemyDirector;
	public GameObject windLocs;
	public AnimationCurve waveSizePerLevel;
	public AnimationCurve maxEnemiesPerLevel;
	public int maxLevels;
	public int maxWaveSize;
	public int maxEnemies;

	List<WindowBehavior> windows;
	int currentLevel;

	// Use this for initialization
	void Start () {
		currentLevel = 0;
		instance = this;
		
		windows = new List<WindowBehavior> ();

		setUpLevel ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setUpLevel()
	{
		currentLevel++;

		windows.Clear ();

		foreach (Transform t in windLocs.transform) 
		{
			windows.Add(t.GetComponent<WindowBehavior>());
		}

		enemyDirector.waveSize = (int) (waveSizePerLevel.Evaluate (((float)currentLevel) / maxLevels) * maxWaveSize);
		enemyDirector.totalEnemies = (int) (maxEnemiesPerLevel.Evaluate(((float)currentLevel)/maxLevels) * maxEnemies);

		for(int i =  0; i < windows.Count; i++)
		{
			windows[i].refreshHealth();
		}

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

}
