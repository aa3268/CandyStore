using UnityEngine;
using System.Collections;

public class CameraSpring : MonoBehaviour {

	public GameObject cam;
	public GameObject origin;

	public float distanceObject;
	public float distancePlayer;

	public float limit;
	public float limit2;
	public RaycastHit hit;

	public float pos_y_close;
	public float pos_z_close;
	public float pos_y_far;
	public float pos_z_far;

	public Quaternion rotationClose;
	public Quaternion rotationFar;

	public bool enter;
	// Use this for initialization
	void Start () {
		enter = false;
		rotationFar = new Quaternion (0, 0, 0, 0);
		rotationFar.eulerAngles = new Vector3 (25, 0, 0);

		rotationClose = new Quaternion (0, 0, 0, 0);
		rotationClose.eulerAngles = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
			
			Physics.Raycast (origin.transform.position, origin.transform.forward, out hit);
			distanceObject = hit.distance ;
			

		if ((distanceObject < limit && cam.transform.localPosition.y > pos_y_close && cam.transform.localPosition.z < pos_z_close) || enter) {

			cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, pos_y_close, pos_z_close), Time.deltaTime * 2f);
			cam.transform.localRotation = Quaternion.Slerp(cam.transform.localRotation, rotationClose, Time.deltaTime);
			//cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(0, 4, -4), ratio);
		}
		if (distanceObject > 1.5*limit && cam.transform.localPosition.y < pos_y_far && cam.transform.localPosition.z > pos_z_far) {
		
			cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, pos_y_far, pos_z_far), Time.deltaTime * 2f);
			cam.transform.localRotation = Quaternion.Slerp(cam.transform.localRotation, rotationFar, Time.deltaTime);
			//cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(0, 2, -2), ratio);
		}
	}

	void OnTriggerEnter(Collider obj)
	{
		enter = true;
	}

	void OnTriggerExit(Collider obj)
	{
		enter = false;
	}
}
