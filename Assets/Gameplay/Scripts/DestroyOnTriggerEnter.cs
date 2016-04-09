using UnityEngine;
using System.Collections;

/*! \brief Destroys an object with tag equal to &lt;targetTag&gt; upon _OnTriggerEnter_
*/
[RequireComponent (typeof (Collider))] public class DestroyOnTriggerEnter : MonoBehaviour
{
	public string targetTag; //!< the objects with the "tag" will trigger its destruction


	void OnTriggerEnter (Collider other)
	{
		// If we have triggered with an object with "tag", we freeze our surroundings
		if (other.CompareTag (targetTag))
		{
			Destroy (other.gameObject);
		}
	}
}
