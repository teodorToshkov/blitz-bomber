using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/*! \brief A script which casts a lightning, which goes through floors below the current one.
 * 
 * This script is responsible for determining the behaviour of the floors with a "lightning" buff.\n
 * 
 * Upon trigger it casts a lightning onto floors below it.
 */
public class LightningFloorScript : Floor
{
	public GameObject destroyedFloor; //!< A __GameObject__ we will spawn on the places of the ones which are destroyed from the lightning

	//! We 'cast a lightning' downwards
	/*!
	 We loop pick a random Floor and if its position on the __y__ axis is less than that of our last destroyed Floor, we check if there is
	 a Floor between the the two. If there is, we destroy the Floor which is in the way, otherwise we destroy the one we picked randomly.\n
	 This continues until the 'bottom' __Floors__ have been reached.

	 Note: In order to prevent the posibility of an infinate loop, we set a limit of 30 iterations.
	*/
	public override void Destroy ()
	{
		// We store a counter in order to prevent an infinate loop in case the algorithm fails
		int counter = 0;

		Floor floor = null;
		while ((floor == null || floor.transform.position.y > -6) && counter++ < 30)
		{
			try
			{
				Floor buffer = GameManager.GetRandomFloor ();
				if (buffer.CompareTag ("Destroyable") && buffer.gameObject.activeSelf)
				{
					if (floor == null && buffer.transform.position.y < transform.position.y)
					{
						buffer = GetFloorInWay (GetComponent<Floor> (), buffer);
						InstantiateLinkBetweenFloors (buffer, this);
						floor = buffer;
					}
					else if (buffer.transform.position.y < floor.transform.position.y)
					{
						buffer = GetFloorInWay (floor, buffer);
						InstantiateLinkBetweenFloors (floor, buffer);
						GameManager.RemoveFloor (floor);
						floor = buffer;
					}
				}
			}
			catch (NullReferenceException)
			{
				continue;
			}
		}
		GameManager.RemoveFloor (floor);

		base.Destroy ();
	}

	//! A function which returns a __Floor__ which is in the way from one __Floor__ to another
	/*!
	 We cast a ray from the __start floor__ towards the __end floor__ and if we hit a __Floor__, we return it, if not, we return the __end floor__.
	 @returns the Floor which passes through the _line_, connecting the __start floor__ and the __end floor__, if any or the __end floor__ itself
	 */
	private Floor GetFloorInWay (Floor start /*!< the __Floor__ from which we will start the ray */,
								 Floor end /*!< the __Floor__ at which we will end the ray */)
	{
		Vector3 startPos = start.transform.position;
		Vector3 endPos = end.transform.position;
		Physics.queriesHitTriggers = true;
		RaycastHit hit;
		Floor floorToReturn = null;
		if (Physics.Raycast (startPos, endPos - startPos, out hit))
			floorToReturn = hit.collider.gameObject.GetComponent<Floor> ();
		return floorToReturn.CompareTag ("Destroyable") ? floorToReturn : end;
	}

	//! A function which instantiates a new _destroyedFloor_ with rotation and scale so that if links the __start floor__ and the __end floor__
	/*!
	 */
	private void InstantiateLinkBetweenFloors (Floor start /*!< the __Floor__ from which we will start the ray */,
											   Floor end /*!< the __Floor__ at which we will end the ray */)
	{
		Vector3 startPos = start.transform.position;
		Vector3 endPos = end.transform.position;

		Vector3 newPos = Vector3.Lerp (startPos, endPos, 0.5f);
		float newScale = Vector3.Distance (startPos, endPos);

		GameObject newLink = Instantiate (destroyedFloor, newPos, Quaternion.identity) as GameObject;
		newLink.transform.LookAt (end.transform);
		newLink.transform.localScale = new Vector3 (0.15f, 0.15f, newScale);
	}
}
