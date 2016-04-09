using UnityEngine;
using System.Collections;

/*! \brief Destroys the __gameObject__ it is attached to after a given time interval.
*/
public class DestroyAfter : MonoBehaviour
{
	public float time = 1; //!< the time after which the __gameObject__ will be destroyed

	//! Invokes the function _DestroySelf_ after the __time__
	void Start () {
		Invoke ("DestroySelf", time);
	}

	//! Destroys the __gameObject__, the script is attached to.
	void DestroySelf ()
	{
		Destroy (gameObject);
	}
}
