using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
/*! \brief Destroys __Floors__ upon _OnTriggerEnter_
*/
public class DestroyFloorOnTriggerEnter : MonoBehaviour
{
	//! Destroys __Floors__ upon trigger
	void OnTriggerEnter (Collider other)
	{
		Floor floor = other.GetComponent <Floor> ();
		if (floor != null && other.transform.position.y > -6)
			GameManager.RemoveFloor (floor);
	}
}
