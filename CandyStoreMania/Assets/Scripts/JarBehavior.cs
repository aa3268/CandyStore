<<<<<<< HEAD
﻿using UnityEngine;
using System.Collections;

public class JarBehavior : MonoBehaviour {


	public bool fall;

	void Start()
	{
		fall = false;
	}

	void Update()
	{
		if (fall) {
			Fall();
		}
	}
	// Use this for initialization
	void OnCollisionEnter(Collision obj)
	{
		GetComponent<BoxCollider> ().isTrigger = true;
		fall = true;
	}


	void Fall()
	{
		transform.position = Vector3.Lerp (transform.localPosition, new Vector3 (transform.position.x, 0.1f, transform.position.z), Time.deltaTime);
	}
}
=======
﻿using UnityEngine;
using System.Collections;

public class JarBehavior : MonoBehaviour {


	public bool fall;
	public BoxCollider col;

	void Start()
	{
		fall = false;
	}

	void Update()
	{
		if (fall) {
			Fall();
		}
	}
	// Use this for initialization
	void OnCollisionEnter(Collision obj)
	{
		col.isTrigger = true;
		fall = true;
	}


	void Fall()
	{
		transform.position = Vector3.Lerp (transform.localPosition, new Vector3 (transform.position.x, 0.1f, transform.position.z), Time.deltaTime);
	}
}
>>>>>>> origin/master
