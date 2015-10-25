using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	public int maxHealth;

	int currentHealth;
	bool depleted;
	Slider healthBar;

	Vector3 attachedPosition;

	Camera cam;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		depleted = false;
		healthBar = GetComponent<Slider> ();
		healthBar.maxValue = maxHealth;
		healthBar.value = maxHealth;
		cam = GameObject.Find ("MainCamera").GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		setPosition ();
	}

	public void doDamage(int damage)
	{
		currentHealth -= damage;
		if(currentHealth <= 0)
		{
			depleted = true;
			currentHealth = 0;
		}

		healthBar.value = currentHealth;

	}

	public bool isDepleted()
	{
		return depleted;
	}

	public int getMaxHealth()
	{
		return maxHealth;
	}

	public void setMaxHealth(int max)
	{
		if(currentHealth == maxHealth)
		{
			currentHealth = max;
		}

		maxHealth = max;
	}

	public void reFillHealth()
	{
		currentHealth = maxHealth;
		healthBar.value = currentHealth;
	}

	void setPosition()
	{
		Vector3 screenPos = cam.WorldToScreenPoint (attachedPosition);

		transform.position = new Vector3 (screenPos.x, screenPos.y, screenPos.z);
	}

	public void setAttachedObjectPos(Vector3 pos)
	{
		attachedPosition = pos;
	}

}
