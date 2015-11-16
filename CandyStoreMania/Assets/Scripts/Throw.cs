using UnityEngine;
using System.Collections;

public class Throw : MonoBehaviour {


	public int ammo = 2;
	public float cooldown = 5f;
	public float timer;


	public GameObject bombPrefab;
	public GameObject bombTemp;
	public GameObject left;

	public Quaternion rotation;
	public Vector3 position;
	// Use this for initialization
	void Start () {
		rotation = new Quaternion (0, 0, 0,0);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftAlt) && timer >= cooldown && ammo > 0) {
			bombTemp = (GameObject) Instantiate(bombPrefab, left.transform.position, rotation);
			 
			bombTemp.GetComponent<Rigidbody>().AddForce(left.transform.forward * 50f);
			timer = 0f;
			ammo -= 1;
		}

		if (timer < cooldown) {
			timer += Time.deltaTime;
		}
	}
}
