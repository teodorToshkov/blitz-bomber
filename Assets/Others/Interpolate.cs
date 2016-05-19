/*! \brief A class for interpolating between two floats.
 * 
 * We can choose between liear, ease-in, ease-out and ease-in-out interpolations.
 */
public static class Interpolate
{
	public enum AnimationType
	{
		linear,		//!< The animation has the same speed from start to end
		easeIn,		//!< The animation has a slow start
		easeOut,	//!< The animation has a slow end
		easeInOut,	//!< The animation has a slow start and end
	}

	/// <summary>
	/// Linearly interpolates between 2 floats.
	/// </summary>
	/// <returns>The appropriate point.</returns>
	/// <param name="startValue">Start value.</param>
	/// <param name="endValue">End value.</param>
	/// <param name="currentTime">Current time.</param>
	public static float Linear (float startValue, float endValue, float currentTime, float duration)
	{
		float changeInValue = endValue - startValue;
		return changeInValue * currentTime / duration + startValue;
	}

	/// <summary>
	/// Interpolates between 2 floats with a slow start and increases speed towards the end.
	/// </summary>
	/// <returns>The appropriate point.</returns>
	/// <param name="startValue">Start value.</param>
	/// <param name="endValue">End value.</param>
	/// <param name="currentTime">Current time.</param>
	public static float EaseInQuad (float startValue, float endValue, float currentTime, float duration)
	{
		float changeInValue = endValue - startValue;
		currentTime /= duration;
		return changeInValue * currentTime * currentTime + startValue;
	}

	/// <summary>
	/// Interpolates between 2 floats with a fast start and decreases speed towards the end.
	/// </summary>
	/// <returns>The appropriate point.</returns>
	/// <param name="startValue">Start value.</param>
	/// <param name="endValue">End value.</param>
	/// <param name="currentTime">Current time.</param>
	public static float EaseOutQuad (float startValue, float endValue, float currentTime, float duration)
	{
		float changeInValue = endValue - startValue;
		currentTime /= duration;
		return -changeInValue * currentTime * (currentTime - 2) + startValue;
	}

	/// <summary>
	/// Interpolates between 2 floats with a slow start and a slow end.
	/// </summary>
	/// <returns>The appropriate point.</returns>
	/// <param name="startValue">Start value.</param>
	/// <param name="endValue">End value.</param>
	/// <param name="currentTime">Current time.</param>
	public static float EaseInOutQuad (float startValue, float endValue, float currentTime, float duration)
	{
		float changeInValue = endValue - startValue;
		currentTime /= duration / 2;
		if (currentTime < 1) return changeInValue / 2 * currentTime * currentTime + startValue;
		currentTime--;
		return -changeInValue / 2 * (currentTime * (currentTime - 2) - 1) + startValue;
	}
}
