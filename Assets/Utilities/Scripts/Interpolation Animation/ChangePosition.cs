using UnityEngine;
using System.Collections;

/*! \brief This script is responsible for changing a __GameObject__'s position.
 */
public class ChangePosition : MonoBehaviour
{
	public Interpolate.AnimationType type;	//!< The type of animation we will use
	public float duration;		//!< The duration of the animation

	private float startTime;		//!< The time at which the animation starts
	private Vector3 startPosition;	//!< The position at which the animation starts
	private Vector3 currentPosition;//!< A holder for the current position, which we can manipulate more easily
	private Vector3 targetPosition;	//!< The position which we are aiming at

	private float interval = 0.02f;	//!< The intervals of time we are going to update the position at

    void Start ()
    {
        startPosition = transform.localPosition;
        currentPosition = transform.localPosition;
        targetPosition = transform.localPosition;
    }

    /// <summary>
    /// Sets the position of the GameObject.
    /// </summary>
    /// <param name="position">The new position of the GameObject.</param>
    public void Set(Vector3 position)
    {
        targetPosition = position;

        startTime = Time.time;
        startPosition = transform.localPosition;
        LoopAnimation();
    }

    /// <summary>
    /// Sets the position of the GameObject.
    /// </summary>
    /// <param name="position">The position in the form "X;Y;Z".</param>
    public void Set(string position)
    {
        string[] buffer = position.Split(';');
        Vector3 newPosition;
        newPosition.x = float.Parse(buffer[0]);
        newPosition.y = float.Parse(buffer[1]);
        newPosition.z = float.Parse(buffer[2]);

        Set(newPosition);
    }

    public void SetTarget(GameObject target)
    {
        Vector3 newPosition = target.transform.position - transform.position + transform.localPosition;
        Set(newPosition);
    }

    /// <summary>
    /// Multiplies the position by the given factor.
    /// </summary>
    /// <param name="factor">The number, by which we will multiply the position.</param>
    public void MultiplyBy (float factor)
	{
		targetPosition = transform.localPosition * factor;

		startTime = Time.time;
		startPosition = transform.localPosition;
		LoopAnimation ();
	}

	/// <summary>
	/// Increases the X position with the given amount.
	/// </summary>
	/// <param name="amount">Amount to increase with.</param>
	public void IncreaseXPositionWith (float amount)
	{
		targetPosition.x += amount;

		startTime = Time.time;
		startPosition = transform.localPosition;
		LoopAnimation ();
	}

	/// <summary>
	/// Increases the Y position with the given amount.
	/// </summary>
	/// <param name="amount">Amount to increase with.</param>
	public void IncreaseYPositionWith (float amount)
	{
        targetPosition.y += amount;

        startTime = Time.time;
		startPosition = transform.localPosition;
		LoopAnimation ();
	}

	/// <summary>
	/// Increases the Z position with the given amount.
	/// </summary>
	/// <param name="amount">Amount to increase with.</param>
	public void IncreaseZPositionWith (float amount)
	{
        targetPosition.z += amount;

        startTime = Time.time;
		startPosition = transform.localPosition;
		LoopAnimation ();
	}

	/// <summary>
	/// Plays an animation with given starting and ending positions.
	/// </summary>
	/// <param name="initialPosition">Initial position.</param>
	/// <param name="finalPosition">The position, which we are aiming at.</param>
	public void PlayAnimation (Vector3 initialPosition, Vector3 finalPosition)
	{
		transform.localPosition = initialPosition;
		targetPosition = finalPosition;

		startTime = Time.time;
		startPosition = transform.localPosition;
		LoopAnimation ();
	}

	/// <summary>
	/// Plays an animation with given starting and ending positions.
	/// </summary>
	/// <param name="initialPosition">Initial position.</param>
	/// <param name="finalPosition">The position, which we are aiming at.</param>
	public void PlayAnimation (float initialPosition, float finalPosition)
	{
		transform.localPosition = new Vector3 (initialPosition, initialPosition, initialPosition);
		targetPosition = new Vector3 (finalPosition, finalPosition, finalPosition);

		startTime = Time.time;
		startPosition = transform.localPosition;
		LoopAnimation ();
	}

	/// <summary>
	/// Plaies an animation with a given target position.
	/// </summary>
	/// <param name="finalPosition">The position, which we are aiming at.</param>
	public void PlayAnimation (Vector3 finalPosition)
	{
		targetPosition = finalPosition;

		startTime = Time.time;
		startPosition = transform.localPosition;
		LoopAnimation ();
	}

	/// <summary>
	/// Plaies an animation with a given target position.
	/// </summary>
	/// <param name="finalPosition">The position, which we are aiming at.</param>
	public void PlayAnimation (float finalPosition)
	{
		targetPosition = new Vector3 (finalPosition, finalPosition, finalPosition);

		startTime = Time.time;
		startPosition = transform.localPosition;
		LoopAnimation ();
	}

	/// <summary>
	/// Loops the animation.
	/// </summary>
	void LoopAnimation ()
	{
		if (Time.time < startTime + duration)
		{
			switch (this.type)
			{
				case Interpolate.AnimationType.linear:
					currentPosition.x = Interpolate.Linear (startPosition.x, targetPosition.x, Time.time - startTime, duration);
					currentPosition.y = Interpolate.Linear (startPosition.y, targetPosition.y, Time.time - startTime, duration);
					currentPosition.z = Interpolate.Linear (startPosition.z, targetPosition.z, Time.time - startTime, duration);
					break;
				case Interpolate.AnimationType.easeIn:
					currentPosition.x = Interpolate.EaseInQuad (startPosition.x, targetPosition.x, Time.time - startTime, duration);
					currentPosition.y = Interpolate.EaseInQuad (startPosition.y, targetPosition.y, Time.time - startTime, duration);
					currentPosition.z = Interpolate.EaseInQuad (startPosition.z, targetPosition.z, Time.time - startTime, duration);
					break;
				case Interpolate.AnimationType.easeOut:
					currentPosition.x = Interpolate.EaseOutQuad (startPosition.x, targetPosition.x, Time.time - startTime, duration);
					currentPosition.y = Interpolate.EaseOutQuad (startPosition.y, targetPosition.y, Time.time - startTime, duration);
					currentPosition.z = Interpolate.EaseOutQuad (startPosition.z, targetPosition.z, Time.time - startTime, duration);
					break;
				case Interpolate.AnimationType.easeInOut:
					currentPosition.x = Interpolate.EaseInOutQuad (startPosition.x, targetPosition.x, Time.time - startTime, duration);
					currentPosition.y = Interpolate.EaseInOutQuad (startPosition.y, targetPosition.y, Time.time - startTime, duration);
					currentPosition.z = Interpolate.EaseInOutQuad (startPosition.z, targetPosition.z, Time.time - startTime, duration);
					break;
			}

			transform.localPosition = currentPosition;
			Invoke ("LoopAnimation", interval);
		}
		else
		{
			transform.localPosition = targetPosition;
		}
	}
}