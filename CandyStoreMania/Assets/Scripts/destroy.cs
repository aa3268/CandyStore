using UnityEngine;
using System.Collections;

public class destroy : MonoBehaviour {

	// Use this for initialization
	void OnParticleCollision(GameObject other)
	{
		if (other.name.Contains ("Cube")) {
			Destroy (other);
		}
	}
}
