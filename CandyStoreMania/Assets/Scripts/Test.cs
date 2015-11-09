using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {


	public Quaternion start;
	public Quaternion target;
	
	public float time = 0f;

	public bool hit = false;
	public GameObject sp;
	// Use this for initialization
	void Start () {
		
		target = new Quaternion (0.0f, 0.0f, 0.0f, 1.0f);
		start = new Quaternion (0.0f, 0.0f, 0.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {

		start.SetEulerAngles (sp.transform.localEulerAngles);
	
		target.SetEulerAngles (sp.transform.localEulerAngles.x + 1.5f, sp.transform.localEulerAngles.y, sp.transform.localEulerAngles.z );





		if (Input.GetMouseButtonDown (0) && hit == false) {
			hit = true;
		}

		if (hit) {
			sp.transform.rotation = Quaternion.Slerp (start, target, time);
			time += 0.1f;

		} else {
			//sp.transform.rotation = start;
		}

		if (time >= 2) {
			float xa = sp.transform.localEulerAngles.x;
			sp.transform.Rotate(new Vector3(-xa, 0 ,0));
			time = 0f;
			hit = false;
		}
	}
}
