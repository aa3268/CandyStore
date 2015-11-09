using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	
  //Joystick button variables
	public static Player instance;
	public float xr;
	public float yr;
	public float xl;
	public float yl;
	public float button0;
	public float button1;
	public float button2;
	public float button3;
	
    public float movementSpeed = 2.0f;
	public float rotationSpeed = 100f;

	public GameObject right;
	public GameObject left;

	public Camera player;

	public bool paused;
	public RechargeStation reload;

	List<GameObject> unlockedWeapons = new List<GameObject>();
	GameObject activeWeapon;

    void Start () {
		instance = this;
		paused = false;

		if (WeaponsUnit.instance != null) {
			WeaponsUnit.instance.setUnlockedWeapons ();
		}

		activeWeapon = unlockedWeapons [0];
	}
    void Update()
    {

		KeyboardMouseControls ();
		//JoystickControls ();

	}
			
	/*void JoystickControls()
	{
		xr = Input.GetAxis ("RightStickH");
		yr = Input.GetAxis ("RightStickV");
				
		xl = Input.GetAxis ("LeftStickH");
		yl = Input.GetAxis ("LeftStickV");
		
		xr =  -1*Mathf.Round (xr * 100f) / 100f;
		yr =  -1*Mathf.Round (yr * 100f) / 100f;

		xl =  1*Mathf.Round (xl * 100f) / 100f;
		yl =  1*Mathf.Round (yl * 100f) / 100f;

		button0 = Input.GetAxis ("Fire1");
		button1 = Input.GetAxis ("Fire2");

		if (xl > 0.5) {

			transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
		}
		if (xl < -0.5) {

			transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
		}
		
		if (xr > 0.5) {
			
			transform.Translate(Vector3.left *  movementSpeed * Time.deltaTime);
		}
		if (xr < -0.5) {
			
			transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
		}
		if (yr > 0.5) {
			
			transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
		}
		if (yr < -0.5) {
			
			transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
		}

		if (button0 > 0) {
			ps1.Play();
		}
		if (button1 > 0) {
			ps2.Play();
		}
	}*/
	void KeyboardMouseControls()
	{
		if (!paused) {
			float y = Input.GetAxis ("Mouse X");
			float x = transform.rotation.x;
			float z = transform.rotation.z;

			Vector3 r = new Vector3 (x, y, z);
			transform.Rotate (r * 1.5f);

			if (Input.GetKey (KeyCode.W)) {
				transform.Translate (Vector3.forward * Time.deltaTime * movementSpeed);
			}
			if (Input.GetKey (KeyCode.S)) {
				transform.Translate (Vector3.back * Time.deltaTime * movementSpeed);
			}
			if (Input.GetKey (KeyCode.A)) {
				transform.Translate (Vector3.left * Time.deltaTime * movementSpeed);
			}
			if (Input.GetKey (KeyCode.D)) {
				transform.Translate (Vector3.right * Time.deltaTime * movementSpeed);
			}
			if (Input.GetKey (KeyCode.Q)) {
				transform.Rotate (Vector3.down * Time.deltaTime * rotationSpeed);
			}
			if (Input.GetKey (KeyCode.E)) {
				transform.Rotate (Vector3.up * Time.deltaTime * rotationSpeed);
			}
			if (Input.GetKey (KeyCode.Alpha1)) {
				activeWeapon.SetActive (false);
				unlockedWeapons[0].gameObject.SetActive (true);
				activeWeapon = unlockedWeapons[0].gameObject;
			}
			if (Input.GetKey (KeyCode.Alpha2)) {
				Debug.Log (unlockedWeapons.Count);
				if(unlockedWeapons.Count > 2)
				{
					activeWeapon.SetActive (false);
					unlockedWeapons[1].gameObject.SetActive (true);
					activeWeapon = unlockedWeapons[1].gameObject;
				}
			}
			if(Input.GetKeyDown(KeyCode.Space))
			{
				if(Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), 
				                    new Vector3(reload.transform.position.x, 0f, reload.transform.position.z)) < 5f)
				{
					reload.reload(activeWeapon);
				}
			}


		}
			
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			if (!paused) 
			{
				Time.timeScale = 0.00000000000000000000000001f;
				paused = true;
				PauseMenu.instance.ScaleUp();
			} 
			else 
			{
				Time.timeScale = 1;
				paused = false;
				PauseMenu.instance.ScaleDown();
			}
		}
	}

	public void addWeapon(GameObject weapon)
	{
		unlockedWeapons.Add (weapon);
	}

	public int getAmmoCount()
	{
		return activeWeapon.GetComponent<WeaponsInterface> ().getAmmo ();
	}
}
