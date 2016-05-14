using UnityEngine;
using System.Collections;

/*! \brief This script moves the object it is atached to linearly along the X axis at a given speed between 2 positions on the X axis.
 */
public class SwitchBetweenXs : MonoBehaviour
{
	public float x1; //!< the minimal X value
	public float x2; //!< the maximal X value
	[Range(0, 5)]
	public float speed = 0.5f; //!< the speed at which the object will move
	public float gamePlaySpeedEffectorFactor = 1f; //!< the factor by which the speed is affected by GameManager.gamePlaySpeed

	private bool switchDirection = false; //!< marks at which position we are going
	
	//! Update is called once per frame
	void Update ()
	{
		// We don't move if the gameplay is stopped
		if (!GameManager.isPlaying)
			return;

		// We multiply the speed at which the object is moving by the rate at which the gameplay is moving
		float newSpeed = speed * (1 + (GameManager.gamePlaySpeed - 1) * gamePlaySpeedEffectorFactor);
		// We store the current position in a buffer
		Vector3 newPosition = transform.position;
		// If the position is less than the minimum allowed we switch the direction at which we are moving the object
		if (transform.position.x < x1)
			switchDirection = false;
		// If the position is more than the maximum allowed we switch the direction at which we are moving the object
		else if (transform.position.x > x2)
			switchDirection = true;
		// If we are going in the left direction we move the object to the left
		if (switchDirection)
			newPosition.x -= Time.deltaTime * newSpeed;
		// If we are going in the right direction we move the object to the right
		else
			newPosition.x += Time.deltaTime * newSpeed;
		// We update the object's position
		transform.position = newPosition;
	}

	/// <summary>
	/// Increases the x1.
	/// </summary>
	/// <param name="amount">Amount.</param>
	public void IncreaseX1 (float amount)
	{
		x1 += amount;
	}

	/// <summary>
	/// Increases the x2.
	/// </summary>
	/// <param name="amount">Amount.</param>
	/// \todo Make it animated with Interpolate.EaseInOut
	public void IncreaseX2 (float amount)
	{
		x2 += amount;
	}
}
