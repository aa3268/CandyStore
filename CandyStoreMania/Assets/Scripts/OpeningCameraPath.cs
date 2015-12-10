using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class OpeningCameraPath : MonoBehaviour {

	List<Vector3> startingLocation;
	List<Quaternion> startingRotation;

	public List<Camera> cameras;
	public List<GameObject> vitalPoints;
	public List<float> cameraSpeed;
	public Image blackout;

	List<Vector3> checkPoints;
	float pivoted = 0f;
	int currentStep = 0;
	float resetTime = 8f;
	float currentTime = 0f;
	OpeningDialogue script;

	// Use this for initialization
	void Start () {
		checkPoints = new List<Vector3> ();
		startingLocation = new List<Vector3> ();
		startingRotation = new List<Quaternion> ();
		script = GetComponent<OpeningDialogue> ();

		for(int i = 0; i < cameras.Count; i++)
		{
			startingRotation.Add (cameras [i].transform.rotation);
			startingLocation.Add (cameras[i].transform.position);
		}

		checkPoints.Add(new Vector3 (vitalPoints[0].transform.position.x, cameras[0].gameObject.transform.position.y, vitalPoints[0].transform.position.z));
		checkPoints.Add(new Vector3 (vitalPoints[1].transform.position.x, cameras[0].gameObject.transform.position.y, vitalPoints[1].transform.position.z));
		checkPoints.Add(new Vector3 (vitalPoints[2].transform.position.x, cameras[1].gameObject.transform.position.y, vitalPoints[2].transform.position.z));

	}
	
	// Update is called once per frame
	void Update () {

		switch (currentStep) {
			case 0:
				panAroundObject (currentStep, 60f, cameras[0]);
				break;
			case 1:
				zoomAway(currentStep, cameras[0]);
				break;
			case 2:
				cameras[0].enabled = false;
				cameras[1].enabled = true;
				zoomAndRotate(currentStep, cameras[1]);
				break;
			case 3:
				fadeOut();
				break;
			case 4:
				reset();
				break;
			default:
				break;
		}

		if (Input.anyKeyDown) {
			blackout.color = new Color(0,0,0,1f);
			Application.LoadLevel ("main");
		}

	}

	void panAroundObject(int step, float goal, Camera cam)
	{
		cam.gameObject.transform.RotateAround (checkPoints[step], Vector3.down, cameraSpeed[step] * Time.deltaTime);
		cam.gameObject.transform.LookAt (checkPoints[step]);
		Debug.Log (checkPoints [step]);
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

	void zoomAway(int step, Camera cam)
	{
		cam.gameObject.transform.position = Vector3.Lerp (cam.gameObject.transform.position, checkPoints [step], cameraSpeed [step] * Time.deltaTime);

		if(Vector3.Distance(cam.transform.position, checkPoints[step]) < 1.5f)
		{
			currentStep ++;
		}
	}

	void zoomAndRotate(int step, Camera cam)
	{
		float dis = Vector3.Distance (cam.gameObject.transform.position, checkPoints [step]);
		cam.gameObject.transform.position = Vector3.Lerp (cam.gameObject.transform.position, checkPoints [step], cameraSpeed [step]/dis * Time.deltaTime);
		cam.transform.RotateAround (cam.transform.position, Vector3.down, Time.deltaTime * 8f);
		if(Vector3.Distance(cam.transform.position, checkPoints[step]) < 1f)
		{
			currentStep ++;
		}
	}

	void fadeOut()
	{
		blackout.color = Color.Lerp (blackout.color, new Color (0, 0, 0, 1f), Time.deltaTime * 2f);

		Debug.Log (blackout.color.a);
		if (blackout.color.a >= 0.9f) {
			currentStep ++;
		}
	}

	void reset()
	{
		currentTime += Time.deltaTime;
		if(currentTime > resetTime)
		{
			currentTime = 0;
			currentStep = 0;
			script.reset();

			for(int i = 0; i < cameras.Count; i++)
			{
				cameras [i].transform.rotation = startingRotation[i];
				cameras[i].transform.position = startingLocation[i];
			}

			
			cameras[0].enabled = true;
			cameras[1].enabled = false;

			blackout.color = new Color(0, 0, 0, 0);
		}
	}
}
