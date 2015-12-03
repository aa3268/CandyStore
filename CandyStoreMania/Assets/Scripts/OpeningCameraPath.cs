using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpeningCameraPath : MonoBehaviour {

	Vector3 startingLocation;
	Quaternion startingRotation;

	public List<GameObject> vitalPoints;
	public List<float> cameraSpeed;
	List<Vector3> checkPoints;
	float pivoted = 0f;
	int currentStep = 0;
	// Use this for initialization
	void Start () {
		checkPoints = new List<Vector3> ();

		startingRotation = gameObject.transform.rotation;
		startingLocation = gameObject.transform.position;

		for(int i = 0; i < vitalPoints.Count; i++)
		{
			checkPoints.Add(new Vector3 (vitalPoints[i].transform.position.x, gameObject.transform.position.y, vitalPoints[i].transform.position.z));
		}
	}
	
	// Update is called once per frame
	void Update () {

		switch (currentStep) {
			case 0:
				panAroundObject (currentStep, 60f);
				break;
			case 1:
				zoomAway(currentStep);
				break;
			default:
				break;
		}
	}

	void panAroundObject(int step, float goal)
	{
		gameObject.transform.RotateAround (checkPoints[step], Vector3.down, cameraSpeed[step] * Time.deltaTime);
		transform.LookAt (checkPoints[step]);

		if(pivoted < goal)
		{
			pivoted += cameraSpeed[step] * Time.deltaTime;
		}
		else
		{
			currentStep ++;
			pivoted = 0f;
		}
	}

	void zoomAway(int step)
	{
		gameObject.transform.position = Vector3.Lerp (transform.position, checkPoints [step], cameraSpeed [step] * Time.deltaTime);

		if(Vector3.Distance(transform.position, checkPoints[step]) < 0.1f)
		{
			currentStep ++;
		}
	}
}
