using UnityEngine;
using System.Collections;

/*! \brief Casts a __destructionBlast__ with a _targetsize_ equal to __radius__
 */
public class BombFloorScript : Floor
{
	public float radius = 5; //!< the radius of floors which will be destroyed in Unity units
	public float force = 5; //!< the radius of floors which will be destroyed in Unity units
	public ExpandToSize destructionBlast; //!< the visual effect for the destruction radius

	//! We cast a __DestructionBlast__
	public override void Destroy ()
	{
		ExpandToSize newBlast = (Instantiate (destructionBlast.gameObject, transform.position, Quaternion.identity) as GameObject).GetComponent<ExpandToSize> ();
		newBlast.targetSize = radius;

		Collider[] colliders = Physics.OverlapSphere (transform.position, radius);
		foreach (Collider hit in colliders)
		{
			Rigidbody rigidBody = hit.GetComponent<Rigidbody> ();
			if (rigidBody != null && hit.CompareTag ("AffectedByExplosion"))
			{
				rigidBody.AddExplosionForce (force, transform.position, radius, 10.0f);
			}
		}

		base.Destroy ();
	}
}
