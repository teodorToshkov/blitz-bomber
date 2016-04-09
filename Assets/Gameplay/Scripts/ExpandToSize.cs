using UnityEngine;
using System.Collections;

/*! \brief This script makes a __GameObject__ have a start _localScale_ of 0 and expand to __targetSize__
 */
public class ExpandToSize : MonoBehaviour
{
	public float targetSize = 1;
	public float speed = 1;
	private Vector3 targetScale;

	void Start ()
	{
		transform.localScale = Vector3.zero;
		targetScale = new Vector3 (targetSize, targetSize, targetSize);
	}

	// Update is called once per frame
	void Update ()
	{
		transform.localScale = Vector3.Lerp (transform.localScale, targetScale, speed * GameManager.gamePlaySpeed * Time.deltaTime);
	}
}
