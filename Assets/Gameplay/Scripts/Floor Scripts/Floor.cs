using UnityEngine;
using System.Collections;

[RequireComponent(typeof (MoveOnY))]
/*! \brief This script describes a __GameObject__ as a __Floor__ and contains information about its type,
 * 		  whether a Floor.Type.Regular or a special one and also stores references.
 */
public class Floor : MonoBehaviour {

	public enum Type {
		Regular,			//!< Has nothing to brag about
		Bomb,				//!< Destroys everything within a radius
		Lightning,			/*!< Make a small cube going through every _destroyed_ floor along with some other cubes,
								going in random directions, compensating for the scattering of plazma as seen in real-life lightnings
								*/
		Freeze,				//!< Freezes floors within a certain range / Freezes everything but the _dropper_
		TimeSlower,			//!< Time.timeScale = &lt;value&gt;
		SuperBulletsGun,	//!< Indestructible bullets
		Laser,				//!< Destroys everything on its way
		MultipleBulletsGun,	//!< Shoots 3-5-7 bullets at a time in different directions
		RapidFireGun		//!< Fires much faster (maybe 10 times faster)
	};

	[SerializeField]
	public const Type type = Floor.Type.Regular; //!< holds information about the type of __Floor__ we are looking at

	[HideInInspector]
	public MoveOnY moveOnY; //!< a reference to the __MoveOnY__ component of the __GameObject__

	/// <summary>
	/// Sets the reference moveOnY to the component in the __GameObject__: <code>moveOnY = GetComponent<MoveOnY> ();</code>
	/// </summary>
	void Awake ()
	{
		moveOnY = GetComponent<MoveOnY> ();
	}
}
