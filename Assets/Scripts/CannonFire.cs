using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFire : MonoBehaviour {


	public void Fire(GameObject cannonball)
	{
		if (PlayerShip.ammo > 0) {
			PlayerShip.ammo--;
			Instantiate (cannonball, transform.position + transform.forward, gameObject.transform.rotation);
		}
	}
	
}
