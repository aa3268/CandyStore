using UnityEngine;
using System.Collections;

public class Cane : MonoBehaviour {
	
	
	public Quaternion start;
	public Quaternion target;
	
	public float time = 0f;
	
	public bool hit = false;

	// Use this for initialization
	void Start () {
		
		target = new Quaternion (0.0f, 0.0f, 0.0f, 1.0f);
		start = new Quaternion (0.0f, 0.0f, 0.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
		start.SetEulerAngles (transform.localRotation.x,transform.localRotation.y,transform.localRotation.z);
		
		target.SetEulerAngles (transform.localRotation.x + 1.5f,transform.localRotation.y,transform.localRotation.z);
		
		
		
		
		
		if (Input.GetMouseButtonDown (0) && hit == false) {
			hit = true;
		}
		
		if (hit) {
			transform.rotation = Quaternion.Slerp (start, target, time);
			time += 0.2f;
			
		} else {
			//sp.transform.rotation = start;
		}
		
		if (time >= 2) {
			float xa = transform.eulerAngles.x;
			transform.Rotate(new Vector3(-xa, 0 ,0));
			time = 0f;
			hit = false;
		}
	}
}
