using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]
/*! \brief Destroys an object with tag equal to &lt;targetTag&gt; upon _OnTriggerEnter_
*/
public class DestroyOnTriggerEnter : MonoBehaviour
{
	public string targetTag; //!< the objects with the "tag" will trigger its destruction

	//! We destroy the __GameObject__ we triggered with, if its _tag_ matches the __targetTag__
	void OnTriggerEnter (Collider other)
	{
		// If we have triggered with an object with "tag", we freeze our surroundings
		if (other.CompareTag (targetTag))
		{
			Destroy (other.gameObject);
		}
	}
}
