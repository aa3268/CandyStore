using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
	
  //Joystick button variables
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
	public float gun_one_cooldown = 2f;
	public float gun_two_cooldown = 15f;

	public GameObject nozzle;
	public GameObject bulletOnePrefab;
	public GameObject bulletTwoPrefab;
	public GameObject bulletOne;
	public GameObject bulletTwo;

	public GameObject right;
	public GameObject left;

	public Camera player;
	public Camera pause;
	public Camera upgrade;

	public bool paused;
	public static bool gunTwoSelectable = true;

	public GameObject gunOne;
	public GameObject gunTwo;

    void Start () {
		paused = false;
		Cursor.visible = false;
	}
    void Update()
    {

		KeyboardMouseControls ();
		//JoystickControls ();
		if (player.enabled) {
			paused = false;
			Cursor.visible = false;
		} else {
			paused = true;
			Cursor.visible = true;
		}

		if (gun_one_cooldown < 2) {
			gun_one_cooldown += Time.deltaTime * 2f;
		}
		if (gun_two_cooldown < 15) {
			gun_two_cooldown += Time.deltaTime * 5f;
		}
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
			float x = 0;
			float z = 0;
			
			Vector3 v = new Vector3 (x, y, z);
			transform.Rotate (v * 5f);

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
				right.transform.GetChild (1).gameObject.SetActive (true);
				right.transform.GetChild (2).gameObject.SetActive (false);
			}
			if (Input.GetKey (KeyCode.Alpha2)) {
				if(gunTwoSelectable)
				{
				right.transform.GetChild (2).gameObject.SetActive (true);
				right.transform.GetChild (1).gameObject.SetActive (false);
				}
			}

		
			if (Input.GetMouseButtonDown (1)) {


			}
		}
			if (Input.GetKeyDown (KeyCode.Escape)) {
				if (player.enabled == true) {
					player.enabled = false;
					pause.enabled = true;
					Time.timeScale = 0.00000000000000000000000001f;
					Cursor.visible = true;

					
				} else {
					pause.enabled = false;
					player.enabled = true;
					Time.timeScale = 1;
					Cursor.visible = false;
				}
			}
		
	}
}
