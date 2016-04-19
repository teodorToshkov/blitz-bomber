using UnityEngine;
using System.Collections;

/*! \brief A script which is responsible for changing the __Dropper__ to a _laser_ upon destruction of the Floor
 */
public class LaserFloorScript : Floor
{

	//! We 'cast a lightning' downwards
	public override void Destroy ()
	{
		base.Destroy ();
	}
}
