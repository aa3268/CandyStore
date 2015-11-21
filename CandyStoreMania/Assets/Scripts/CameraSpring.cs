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


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
			
			Physics.Raycast (origin.transform.position, origin.transform.forward, out hit);
			distanceObject = hit.distance ;



		if (distanceObject < limit && cam.transform.localPosition.y > pos_y_close && cam.transform.localPosition.z < pos_z_close) {

			//ratio =((limit - distanceObject)/limit);
			cam.transform.Translate(Vector3.forward);
			//cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(0, 4, -4), ratio);
		}
		if (distanceObject > 3*limit && cam.transform.localPosition.y < pos_y_far && cam.transform.localPosition.z > pos_z_far) {
			//ratio =((distanceObject - limit)/limit);
			cam.transform.Translate(Vector3.back);
			//cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(0, 2, -2), ratio);
		}
	}
}
