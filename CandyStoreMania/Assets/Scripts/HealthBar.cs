using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	public int maxHealth;

	public int currentHealth;
	public bool depleted;
	Slider healthBar;

	Vector3 attachedPosition;

	Camera cam;
	public Image fill;

	// Use this for initialization
	void Awake () {
		currentHealth = maxHealth;
		depleted = false;
		healthBar = GetComponent<Slider> ();
		healthBar.maxValue = maxHealth;
		healthBar.value = maxHealth;
		cam = GameObject.Find ("PlayerCamera").GetComponent<Camera> ();
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
		healthBar.maxValue = max;
		maxHealth = max;
	}

	public void reFillHealth()
	{
		currentHealth = maxHealth;
		healthBar.value = currentHealth;
		depleted = false;
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

	public void setColor(Color c)
	{
		fill.color = c;
	}

}
