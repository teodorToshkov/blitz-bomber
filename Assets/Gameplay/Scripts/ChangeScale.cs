using UnityEngine;
using System.Collections;

/*! \brief This script is responsible for changing a __GameObject__'s scale.
 */
public class ChangeScale : MonoBehaviour
{
	public Interpolate.AnimationType type;	//!< The type of animation we will use
	public float duration;		//!< The duration of the animation

	private float startTime;		//!< The time at which the animation starts
	private Vector3 startScale;		//!< The scale at which the animation starts
	private Vector3 currentScale;	//!< A holder for the current scale, which we can manipulate more easily
	private Vector3 targetScale;	//!< The scale which we are aiming at

	private float interval = 0.02f;	//!< The intervals of time we are going to update the scale at

	/// <summary>
	/// Multiplies the scale by the given factor.
	/// </summary>
	/// <param name="factor">The number, by which we will multiply the scale.</param>
	public void MultiplyBy (float factor)
	{
		targetScale = transform.localScale * factor;

		startTime = Time.time;
		startScale = transform.localScale;
		LoopAnimation ();
	}

	/// <summary>
	/// Increases the X scale with the given amount.
	/// </summary>
	/// <param name="amount">Amount to increase with.</param>
	public void IncreaseXScaleWith (float amount)
	{
		targetScale = new Vector3 (transform.localScale.x + amount, transform.localScale.y, transform.localScale.z);

		startTime = Time.time;
		startScale = transform.localScale;
		LoopAnimation ();
	}

	/// <summary>
	/// Increases the Y scale with the given amount.
	/// </summary>
	/// <param name="amount">Amount to increase with.</param>
	public void IncreaseYScaleWith (float amount)
	{
		targetScale = new Vector3 (transform.localScale.x, transform.localScale.y + amount, transform.localScale.z);

		startTime = Time.time;
		startScale = transform.localScale;
		LoopAnimation ();
	}

	/// <summary>
	/// Increases the Z scale with the given amount.
	/// </summary>
	/// <param name="amount">Amount to increase with.</param>
	public void IncreaseZScaleWith (float amount)
	{
		targetScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z + amount);

		startTime = Time.time;
		startScale = transform.localScale;
		LoopAnimation ();
	}

	/// <summary>
	/// Plays an animation with given starting and ending scales.
	/// </summary>
	/// <param name="initialScale">Initial scale.</param>
	/// <param name="finalScale">The scale, which we are aiming at.</param>
	public void PlayAnimation (Vector3 initialScale, Vector3 finalScale)
	{
		transform.localScale = initialScale;
		targetScale = finalScale;

		startTime = Time.time;
		startScale = transform.localScale;
		LoopAnimation ();
	}

	/// <summary>
	/// Plays an animation with given starting and ending scales.
	/// </summary>
	/// <param name="initialScale">Initial scale.</param>
	/// <param name="finalScale">The scale, which we are aiming at.</param>
	public void PlayAnimation (float initialScale, float finalScale)
	{
		transform.localScale = new Vector3 (initialScale, initialScale, initialScale);
		targetScale = new Vector3 (finalScale, finalScale, finalScale);

		startTime = Time.time;
		startScale = transform.localScale;
		LoopAnimation ();
	}

	/// <summary>
	/// Plaies an animation with a given target scale.
	/// </summary>
	/// <param name="finalScale">The scale, which we are aiming at.</param>
	public void PlayAnimation (Vector3 finalScale)
	{
		targetScale = finalScale;

		startTime = Time.time;
		startScale = transform.localScale;
		LoopAnimation ();
	}

	/// <summary>
	/// Plaies an animation with a given target scale.
	/// </summary>
	/// <param name="finalScale">The scale, which we are aiming at.</param>
	public void PlayAnimation (float finalScale)
	{
		targetScale = new Vector3 (finalScale, finalScale, finalScale);

		startTime = Time.time;
		startScale = transform.localScale;
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
					currentScale.x = Interpolate.Linear (startScale.x, targetScale.x, Time.time - startTime, duration);
					currentScale.y = Interpolate.Linear (startScale.y, targetScale.y, Time.time - startTime, duration);
					currentScale.z = Interpolate.Linear (startScale.z, targetScale.z, Time.time - startTime, duration);
					break;
				case Interpolate.AnimationType.easeIn:
					currentScale.x = Interpolate.EaseInQuad (startScale.x, targetScale.x, Time.time - startTime, duration);
					currentScale.y = Interpolate.EaseInQuad (startScale.y, targetScale.y, Time.time - startTime, duration);
					currentScale.z = Interpolate.EaseInQuad (startScale.z, targetScale.z, Time.time - startTime, duration);
					break;
				case Interpolate.AnimationType.easeOut:
					currentScale.x = Interpolate.EaseOutQuad (startScale.x, targetScale.x, Time.time - startTime, duration);
					currentScale.y = Interpolate.EaseOutQuad (startScale.y, targetScale.y, Time.time - startTime, duration);
					currentScale.z = Interpolate.EaseOutQuad (startScale.z, targetScale.z, Time.time - startTime, duration);
					break;
				case Interpolate.AnimationType.easeInOut:
					currentScale.x = Interpolate.EaseInOutQuad (startScale.x, targetScale.x, Time.time - startTime, duration);
					currentScale.y = Interpolate.EaseInOutQuad (startScale.y, targetScale.y, Time.time - startTime, duration);
					currentScale.z = Interpolate.EaseInOutQuad (startScale.z, targetScale.z, Time.time - startTime, duration);
					break;
			}

			transform.localScale = currentScale;
			Invoke ("LoopAnimation", interval);
		}
		else
		{
			transform.localScale = targetScale;
		}
	}
}
