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

	public bool useMouse;
	public float movementSpeed = 2.0f;
	public float rotationSpeed = 100f;
	
	public GameObject right;
	public GameObject left;
	
	public Camera player;
	
	public bool paused;
	public RechargeStation reload;
	public Animator animator;
	
	List<GameObject> unlockedWeapons = new List<GameObject>();
	List<int> unlockedWeaponNums = new List<int>();
	GameObject activeWeapon;
	WeaponsInterface weapon;
	int activeWeaponNum;
	
	void Start () {
		useMouse = false;
		instance = this;
		paused = false;
		useMouse = false;
		
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
		
		Cursor.lockState = CursorLockMode.Confined;


		if (WeaponsUnit.instance != null) {
			WeaponsUnit.instance.setUnlockedWeapons ();
		}
		
		activeWeaponNum = 0;
		activeWeapon = unlockedWeapons [activeWeaponNum];
		weapon = activeWeapon.GetComponent<WeaponsInterface> ();
		animator.SetInteger ("weapon", 1);
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
			if(useMouse)
			{
				float y = Input.GetAxis ("Mouse X");
				float x = transform.rotation.x;
				float z = transform.rotation.z;
				
				Vector3 r = new Vector3 (x, y, z);
				transform.Rotate (r * 1.5f);
			}

			if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.R) )
			{
				animator.SetInteger("direction", 1);
			}
			else if(Input.GetKeyDown(KeyCode.S))
			{
				animator.SetInteger("direction", 2);
			}
			if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
			{
				animator.SetInteger("direction", 3);
			}

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

			if (Input.GetKey (KeyCode.M)) {
				useMouse = !useMouse;
			}
			
			if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
	
				if(activeWeaponNum == unlockedWeapons.Count-1)
				{
					activeWeaponNum = 0;
				}
				else
				{
					activeWeaponNum++;
				}

				activeWeapon.SetActive (false);
				unlockedWeapons[activeWeaponNum].gameObject.SetActive (true);
				activeWeapon = unlockedWeapons[activeWeaponNum].gameObject;
				weapon = activeWeapon.GetComponent<WeaponsInterface> ();
				animator.SetInteger("weapon", unlockedWeaponNums[activeWeaponNum]);
			}
			
			if(Input.GetKeyDown(KeyCode.Space))
			{
				if(Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), 
				                    new Vector3(reload.transform.position.x, 0f, reload.transform.position.z)) <= 8.5f)
				{
					reload.reload(activeWeapon);
				}
			}
			
			if(!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||
			   Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E)))
			{
				animator.SetInteger("direction", 0);
			}
		}
		
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			if (!paused) 
			{
				Time.timeScale = 0.00000000000000000000000001f;
				paused = true;
				PauseMenu.instance.ScaleUp();
				Cursor.visible = true;
			} 
			else 
			{
				Time.timeScale = 1;
				paused = false;
				PauseMenu.instance.ScaleDown();
				Cursor.visible = false;
			}
		}
	}
	
	public void addWeapon(GameObject weapon, int associated)
	{
		unlockedWeaponNums.Add (associated);
		unlockedWeapons.Add (weapon);
	}
	
	public int getAmmoCount()
	{
		return weapon.getAmmo ();
	}
	
	public int getMaxAmmo()
	{
		return weapon.getMaxAmmo ();
	}
	
	public int getBaseDamage()
	{
		return (int)weapon.getBaseDamage ();
	}

	public Animator getAnimator()
	{
		return animator;
	}

	public List<GameObject> getUnlockedWeapons()
	{
		return unlockedWeapons;
	}


}
