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
	public Image bg;

	bool isVisible = true;

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

	public void doWideEffectDamage(int damage)
	{
		currentHealth -= damage;
		Debug.Log (currentHealth + " damaged" + " " + damage);
		if(currentHealth <= 0)
		{
			LevelDirector.instance.updateHealth (-(damage + currentHealth));
			depleted = true;
			currentHealth = 0;
		}
		else
		{
			LevelDirector.instance.updateHealth (-damage);
		}
		
		healthBar.value = currentHealth;
		Debug.Log (healthBar.value);
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
		Vector3 viewpoint = cam.WorldToViewportPoint (attachedPosition);

		if(viewpoint.z > 0 && viewpoint.x > 0 && viewpoint.x < 1 && viewpoint.y > 0 && viewpoint.y < 1)
		{
			Vector3 screenPos = cam.WorldToScreenPoint (attachedPosition);

			transform.position = new Vector3 (screenPos.x, screenPos.y, screenPos.z);

			if(!isVisible)
			{
				foreach(Transform t in healthBar.transform)
				{
					t.gameObject.SetActive(true);
				}
				isVisible = true;
			}
		}
		else
		{
			if(isVisible)
			{
				foreach(Transform t in healthBar.transform)
				{
					t.gameObject.SetActive(false);
				}
				isVisible = false;
			}
		}
	}

	public void setAttachedObjectPos(Vector3 pos)
	{
		attachedPosition = pos;
	}

	public void setColor(Color c)
	{
		fill.color = c;
	}

	public void showHealthbar(bool show)
	{
		healthBar.gameObject.SetActive (show);

	}

}
