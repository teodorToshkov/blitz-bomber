using UnityEngine;
using System.Collections;

/*! \brief Casts a __destructionBlast__ with a _targetsize_ equal to __radius__
 */
public class BombFloorScript : Floor
{
	public float radius = 5; //!< the radius of floors which will be destroyed in Unity units
	public ExpandToSize destructionBlast; //!< the visual effect for the destruction radius

	//! We cast a __DestructionBlast__
	public override void Destroy ()
	{
		ExpandToSize newBlast = (Instantiate (destructionBlast.gameObject, transform.position, Quaternion.identity) as GameObject).GetComponent<ExpandToSize> ();
		newBlast.targetSize = radius;

		base.Destroy ();
	}
}
