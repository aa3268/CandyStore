﻿using UnityEngine;
using System.Collections;

public class CaramelBomb : MonoBehaviour {

	public float duration = 10f;
	public float startAfter = 1f;
	public float slowAmount = 1f;
	public float timeOut = 5f;
	public bool melt;
	// Use this for initialization
	void Start () {
		melt = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.position.y > 0 && transform.position.y < 0.6) {
			GetComponent<Rigidbody>().useGravity = false;
			GetComponent<SphereCollider>().isTrigger = true;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			melt = true;
		}

		if (melt) {
			Melt();
		}

		if (startAfter <= 0) {
			duration -= Time.deltaTime;
		}

		if (duration <= 0) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider obj)
	{
		if (obj.gameObject.name.Contains ("Enemy") && melt) {

			//Destroy(obj.gameObject);
			obj.gameObject.GetComponent<NavMeshAgent> ().speed = slowAmount;
		} else {
			melt = true;
			Melt();
		}

	}

	void Melt()
	{
		transform.localRotation = Quaternion.Euler(Vector3.zero);
		transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(5f, 0.1f, 5f), 0.05f);
		transform.localPosition = Vector3.Lerp (transform.localPosition, new Vector3 (transform.position.x, 0.1f, transform.position.z), 0.5f);
		if (startAfter > 0) {
			startAfter -= Time.deltaTime;
		}
	}
}