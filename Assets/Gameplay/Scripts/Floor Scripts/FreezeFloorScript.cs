using UnityEngine;
using System.Collections;

/*! \brief A script which freezes all floors within a given radius for a given period of time
 * 
 * This script is responsible for determining the behaviour of the floors with a "freeze" buff
 * 
 * Upon trigger it freezes all floors within a certain radius
 * 
 * \todo Implemet this!
 */
public class FreezeFloorScript : Floor
{
	public float radius = 5; //!< the radius of floors which will be frozen in Unity units
	public float time = 1f; //!< the time for which the floors will remain frozen

	//! We freeze
	public override void Destroy ()
	{
		base.Destroy ();
	}
}
