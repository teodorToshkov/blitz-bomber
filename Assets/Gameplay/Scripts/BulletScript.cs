using UnityEngine;
using System.Collections;

/*! \brief A script which is responsible for managing the way the bullets interact with other objects
 * 		   but not for its movement, for that we use MoveOnY
 */
public class BulletScript : MonoBehaviour
{
	public int endurance = 5; //!< How many "Destroyable" objects can the bullet destroy before it itself is destroyed

	//! We use OnTriggerEnter in order to detect when we interact with a floor
	void OnTriggerEnter (Collider other)
	{
		// We compare the tag to see if we hit a floor
		if (other.CompareTag ("Destroyable"))
		{
			// We destroy the floor
			StartCoroutine (RemoveFloor (other.gameObject));
			// We increase the score of the player
			ThreeDNumber.IncreaseWith (1);

			// We decrease the number of floors the bullet can destroy before it itself is being detroyed
			if (--endurance == 0)
				// If the number we can detroy is 0, then the bullet's life is over and we destroy it
				Destroy (gameObject);
		}
	}

	/// <summary>
	/// Destroys a floor after 0.02 sec.
	/// </summary>
	/// <param name="destroyable">The destroyable to destroy.</param>
	IEnumerator RemoveFloor (GameObject destroyable)
	{
		yield return new WaitForSeconds (0.02f);
		GameManager.RemoveFloor (destroyable);
	}
}
