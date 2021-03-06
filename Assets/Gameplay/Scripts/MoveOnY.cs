using UnityEngine;
using System.Collections;

/*! \brief This script is moves the object on the Y axis and the gameplay is not stopped it is attached to
 * 		   by a given speed between -5 and 5 per second
 */
public class MoveOnY : MonoBehaviour
{
	[Range(-10, 5)]
	public float speed = 0.5f; //!< The rate at which the object is going to travel
	public float gamePlaySpeedEffectorFactor = 1f; //!< the factor by which the speed is affected by GameManager.gamePlaySpeed

	[System.NonSerialized]
	public bool isMoving;

	/// <summary>
	/// Makes the __GameObjects__ continue its movement (no smoothing)
	/// </summary>
	public void StartMoving ()
	{
		isMoving = true;
	}

	//! Update is called once per frame
	//! We update the object's position
	void Update ()
    {
        isMoving = GameManager.isPlaying;
        // We shouldn't change the position if the game is on pause or over
        if (!GameManager.isPlaying || !isMoving)
			return;

        // We multiply the speed at which to travel by the rate at which the gameplay should go
        float newSpeed = gamePlaySpeedEffectorFactor == 0 ? speed : speed * (1 + (GameManager.gamePlaySpeed - 1) * gamePlaySpeedEffectorFactor);

        // We use a buffer to set the position to
        Vector3 newPosition = transform.position;
		// We increase the position on Y by the necessary amount
		newPosition.y += newSpeed * Time.deltaTime;
		// And finally we update the object's position
		transform.position = newPosition;
	}
}
