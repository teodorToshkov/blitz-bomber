using UnityEngine;
using System.Collections;

/*! \brief A script which is responsible for making the bullet it is destroyed by indestructible
 */
public class BulletBoosterFloorScript : Floor
{
	//! Changes the BulletScript.endurance to 100 and the color of its material to the color of the Floor.
	void OnTriggerEnter (Collider other)
	{
		BulletScript bullet = other.GetComponent<BulletScript> ();
		if (bullet != null)
		{
			bullet.endurance = 100;
			bullet.gameObject.GetComponent<MeshRenderer> ().material.color = gameObject.GetComponent<MeshRenderer> ().material.color;
		}
	}
}
