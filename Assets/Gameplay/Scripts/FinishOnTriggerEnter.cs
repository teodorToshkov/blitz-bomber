using UnityEngine;
using System.Collections;

/*! \brief This script makes the game session finish when a floor reaches the "Dropper"
 */
public class FinishOnTriggerEnter : MonoBehaviour
{
	public string targetTag; //!< This marks what we have to hit in order to make the game finish

	//! We use OnTriggerEnter in order to detect when we interact with an object with tag <targetTag>
	void OnTriggerEnter (Collider other)
	{
		// We compare the tag to see if we hit a floor
		if (other.CompareTag (targetTag))
		{
			Debug.Log (other.name);
			// If we have, we end the gameplay and show the result to the screen
			GameManager.EndGamePlay ();
			GameManager.ShowResult ();
		}
	}
}
