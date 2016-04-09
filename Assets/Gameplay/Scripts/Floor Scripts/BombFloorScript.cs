using UnityEngine;
using System.Collections;

/*! \brief This script destroys all __Floors__ within a given radius when the __BombFloor__ is destroyed
 */
public class BombFloorScript : Floor
{
	public float radius = 5; //!< the radius of floors which will be destroyed in Unity units
	public ExpandToSize destructionBlast; //!< the visual effect for the destruction radius

	private float startTime;

	void Start ()
	{
		startTime = Time.time;
	}

	//! We destroy all __Floors__ withing the given radius and instantiate the visual effect
	public override void OnDestruction ()
	{
		ExpandToSize newBlast = (Instantiate (destructionBlast.gameObject, transform.position, Quaternion.identity) as GameObject).GetComponent<ExpandToSize> ();
		newBlast.targetSize = radius;
	}
}
