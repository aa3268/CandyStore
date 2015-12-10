using UnityEngine;
using System.Collections;

public class CannonParticles : MonoBehaviour {
	
	void OnParticleCollision(GameObject obj)
	{
		if(obj.gameObject.name.Contains("Enemy"))
		{
			obj.GetComponent<EnemyScript>().reactToBullet(Player.instance.getBaseDamage());
		}
	}
}
