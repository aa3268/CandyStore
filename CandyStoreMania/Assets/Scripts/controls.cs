using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class controls : MonoBehaviour {


	public sc1 script_one;

	public float xr;
	public float yr;
	public float xl;
	public float yl;
	public float button0;
	public float button1;
	public float button2;
	public float button3;

	public float mvmt = 2.0f; 
	public float rtate = 100f;
    public float movementSpeed = 2.0f;
	public float rotationSpeed = 100f;

	public bool  jumping = false;
	
    public ParticleSystem ps;
    public ParticleSystem ps1;

	public Text t;

	public int size;
	public bool near;

	private bool useKeyboardMouse = true;
	private Rigidbody rb;

	public GameObject sp;
	public GameObject cy;
	
    void Start () {
		sp = GameObject.Find ("Sphere1");
		cy = GameObject.Find ("cube_y");

		near = false;
        rb = GetComponent<Rigidbody>();
		size = Input.GetJoystickNames ().Length;
		if (Input.GetJoystickNames ().Length > 0) {
			useKeyboardMouse = false;
		}
	}
    void Update()
    {
		if (useKeyboardMouse) {
			KeyboardMouseControls ();
		} else {
			JoystickControls();
		}
    }
	
	void JoystickControls()
	{
		sp = GameObject.Find ("Sphere1");
		cy = GameObject.Find ("cube_y");

		xr = Input.GetAxis ("RightStickH");
		yr = Input.GetAxis ("RightStickV");
		
		xl = Input.GetAxis ("LeftStickH");
		yl = Input.GetAxis ("LeftStickV");
		
		xr =  -1*Mathf.Round (xr * 100f) / 100f;
		yr =  -1*Mathf.Round (yr * 100f) / 100f;

		button0 = Input.GetAxis ("Fire1");
		
		xl =  1*Mathf.Round (xl * 100f) / 100f;
		yl =  1*Mathf.Round (yl * 100f) / 100f;
		if (xl > 0.5) {
			transform.Rotate(Vector3.up * rtate * Time.deltaTime);
		}
		if (xl < -0.5) {
			transform.Rotate(Vector3.down * rtate * Time.deltaTime);
		}
		
		if (xr > 0.5) {
			
			transform.Translate(Vector3.left *  mvmt * Time.deltaTime);
		}
		if (xr < -0.5) {
			
			transform.Translate(Vector3.right * mvmt * Time.deltaTime);
			
		}
		if (yr > 0.5) {
			
			transform.Translate(Vector3.forward * mvmt * Time.deltaTime);
		}
		if (yr < -0.5) {
			
			transform.Translate(Vector3.back * mvmt * Time.deltaTime);
			
		}

		if (button0 > 0 && near) {
			sp.GetComponent<Rigidbody>().AddForce(Vector3.up * 50);
		}
				

	}
	void KeyboardMouseControls()
	{
		sp = GameObject.Find ("Sphere1");
		cy = GameObject.Find ("cube_y");

		CheckObj ();
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
		if (Input.GetKey (KeyCode.F)) {
			if(near)
			{
				script_one.moveit = true;
			}
		}
		
		
		if (Input.GetKey (KeyCode.Space)) {
			if (!jumping) {
				jumping = true;
				rb.AddForce (Vector3.up * 100);
			}
		}
		if (Input.GetMouseButtonDown (0)) {
			ps.Play ();
		}
		if (Input.GetMouseButtonDown (1)) {
			ps1.Play ();
		}
		
		
		
		if (transform.position.y < 1.1) {
			jumping = false;
		}

	}
	
	void CheckObj()
	{
		/*
		if (Vector3.Distance (transform.position, sp.transform.position) < 3) {
			near = true;
			if (useKeyboardMouse) {
				t.text = "Press F to interact";
			} else {
				t.text = "Press A to interact";
			}

		} else {
			t.text = "";
			near = false;
		}*/

		if (Vector3.Distance (transform.position, cy.transform.position) < 3) {
			near = true;
			if (useKeyboardMouse) {
				t.text = "Press F to interact";
			} else {
				t.text = "Press A to interact";
			}
			
		} else {
			t.text = "";
			near = false;
		}




	}
}
