using UnityEngine;
using System.Collections;

/*! \brief This script makes a __GameObject__ have a start _localScale_ of 0 and expand to __targetSize__
 */
public class ExpandToSize : MonoBehaviour
{
	public float targetSize = 1;	//!< the size, which we are going to expand towards
	public float speed = 1;			//!< the rate at which we are going to scale the object
	private Vector3 targetScale;	//!< a __Vector3__ representation of the __targetSize__

	//! We initialize the __targetScale__ and set the initial _localScale_ to 0
	void Start ()
	{
		transform.localScale = Vector3.zero;
		targetScale = new Vector3 (targetSize, targetSize, targetSize);
	}

	//! Lerps the _localScale_ of the __GameObject__ towards the __targetScale__
	void Update ()
	{
		transform.localScale = Vector3.Lerp (transform.localScale, targetScale, speed * GameManager.gamePlaySpeed * Time.deltaTime);
	}
}
