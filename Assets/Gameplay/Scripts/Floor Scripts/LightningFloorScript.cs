using UnityEngine;
using System.Collections;

/*! \brief A script which casts a lightning, which goes through floors below the current one.
 * 
 * This script is responsible for determining the behaviour of the floors with a "lightning" buff.\n
 * 
 * Upon trigger it casts a lightning onto floors below it.
 * 
 * \todo Substitute the floor replacement system with a one where a _line_, connecting two consequetive destroyed floors is a block spreading from one to the other
 */

public class LightningFloorScript : Floor
{
	public string targetTag; //!< the objects with the _tag_ will trigger the freeze
	public GameObject destroyedFloor; //!< A __GameObject__ we will spawn on the places of the ones which are destroyed from the lightning

	void OnTriggerEnter (Collider other)
	{
		// If we have triggered with an object with "tag", we 'cast' a lightning
		if (other.CompareTag (targetTag))
		{
			Floor floor = null;
			while (floor == null || floor.transform.position.y > -3)
			{
				Floor buffer = GameManager.floors [Random.Range (0, GameManager.floors.Count - 1)];
				if (buffer != null && buffer.transform != null)
				{
					if (floor == null && buffer.transform.position.y < transform.position.y)
					{
						buffer = GetFloorInWay (GetComponent<Floor> (), buffer);
						floor = buffer;
					}
					else if (buffer.transform.position.y < floor.transform.position.y)
					{
						buffer = GetFloorInWay (floor, buffer);
						Instantiate (destroyedFloor, floor.transform.position, Quaternion.identity);
						GameManager.RemoveFloor (floor);
						floor = buffer;
					}
				}
			}
			Instantiate (destroyedFloor, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}

	//! A function which returns a __Floor__ which is in the way from one __Floor__ to another
	/*!
	 We cast a ray from the __start floor__ towards the __end floor__ and if we hit a __Floor__, we return it, if not, we return the __end floor__.
	 @returns the Floor which passes through the _line_, connecting the __start floor__ and the __end floor__, if any or the __end floor__ itself
	 */
	public Floor GetFloorInWay (Floor start /*!< the __Floor__ from which we will start the ray */,
								Floor end /*!< the __Floor__ at which we will end the ray */)
	{
		Vector3 startPos = start.transform.position;
		Vector3 endPos = end.transform.position;
		Physics.queriesHitTriggers = true;
		RaycastHit hit;
		if (Physics.Raycast (startPos, endPos - startPos, out hit))
			return hit.collider.gameObject.GetComponent<Floor> ();
		return end;
	}
}
