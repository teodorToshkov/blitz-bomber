﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/*! \brief This script is the manager that controls the main characteristics and behaviours of the gameplay\n
 * 		  and is what other scripts refer to when they need information about the state of the game.
 * 
 * It increases the rate at which everything is moving.\n
 * It holds information about the state of the game:
 * 	- whether the user is playing or not / whether the game is over or on pause or not;
 * 	- whether the game is over or not;
 * 	- what the current speed at which the gameplay is going is.
 * 
 * It stores a list of all floors, where we control how the floors are added/removed from the scene.
 */
public class GameManager : MonoBehaviour
{
	public float gamePlayQuickenRate = 0.01f;	//!< the rate at which the gameplay quickens
	public float maxGamePlaySpeed = 2f;			//!< the maximum speedm the gameplay can reach
	public Floor[] _floorPrefabs;				//!< __Prefabs__ of all floors we could instantiate

	public static bool isPlaying;				//!< stores information whether the player playing or not
	public static bool isFinished;				//!< stores information whether the gameplay has or not reached an end i.e. the game is over
	public static float gamePlaySpeed;			//!< the current speed of the gameplay, which is being increased through time
	public static Dictionary<Floor.Type, Floor> floorPrefabs; //!< a static __Dictionary__ representation of **_floorPrefabs**
	public static List<Floor> floors; /*!< A list of all floors which we have spawned
										* regardless of whether or not they are currently active in the scene.\n
										* A floor is never destroyed, a floor is just being set as inactive and reused later.
										*/

	//! Initialization
	/*! 
	 We set that the user is playing and that the game is not over
	 We set the initial gameplay speed to 1
	*/
	void Start ()
	{
		isPlaying = true;
		isFinished = false;
		gamePlaySpeed = 1.0f;
		floors = new List<Floor> (100);
		floorPrefabs = new Dictionary<Floor.Type, Floor> (10);
		foreach (Floor floor in _floorPrefabs)
		{
			if (!floorPrefabs.ContainsKey (floor.type))
				floorPrefabs.Add (floor.type, floor);
			else
				Debug.LogError ("Key " + floor.type + " already exists. Floor name: " + floor.name);
		}
	}

	//! We update the rate at which the gameplay is going every frame if the user is playing
	void Update ()
	{
		// We chech if the game is on pause or not
		if (isPlaying)
		{
			// We increase the rate at which the gameplay is going if it does not excede the maximum we have set
			gamePlaySpeed = Mathf.Min (maxGamePlaySpeed, gamePlaySpeed + Time.deltaTime * gamePlayQuickenRate);
		}
	}

	/// <summary>
	/// Returns the floor at a given index.
	/// </summary>
	/// <returns>The floor.</returns>
	/// <param name="index">Index.</param>
	public static Floor GetFloor (int index)
	{
		return floors [index];
	}

	/// <summary>
	/// Picks a random active floor.
	/// </summary>
	/// <returns>The random floor.</returns>
	public static Floor GetRandomFloor ()
	{
		int rand = Random.Range (0, floors.Count - 1);
		foreach (var floor in floors) {
			if (floor.gameObject.activeSelf && --rand <= 0)
				return floor;
		}
		return floors[0];
	}

	/// <summary>
	/// Adds a new floor to the list.
	/// </summary>
	/// <param name="floorType">Floor type.</param>
	/// <param name="position">Position at which to spawn the floor</param>
	public static void AddFloor (Floor.Type floorType, Vector3 position, Transform parentObject)
	{
		foreach (Floor floor in floors)
		{
			if (!floor.gameObject.activeSelf && floor.type == floorType)
			{
				floor.gameObject.SetActive (true);
				floor.transform.localPosition = position;
				return;
			}
		}
		Floor newFloor;
		if (floorPrefabs.TryGetValue (floorType, out newFloor))
		{
			GameObject newGameObj = GameObject.Instantiate (newFloor.gameObject) as GameObject;
			floors.Add (newGameObj.GetComponent<Floor> ());
			newGameObj.transform.SetParent (parentObject);
			newGameObj.transform.localPosition = position;
		}
		else Debug.LogError ("Floor.Type " + floorType + "was not found!");
	}

	//! Removes a floor from the list and destroys it
	public static void RemoveFloor (GameObject floor)
	{
		RemoveFloor (floor.GetComponent<Floor> ());
	}

	//! Removes a floor from the list and destroys it
	public static void RemoveFloor (Floor floor)
	{
		if (floor == null)
			return;
		floor.Destroy ();
	}

	//! Stops everything withing the gameplay
	public static void StopGamePlay ()
	{
		isPlaying = false;
	}

	//! Resumes or restarts the gameplay
	public static void ResumeGamePlay ()
	{
		isPlaying = true;
		// If the game is over, we need to restart the gameplay
		if (isFinished)
		{
			// We reset the score and reload the level
			ThreeDNumber.ResetNumber ();
			Time.timeScale = 1;
			SceneManager.LoadScene (0);
		}
	}

	//! We stop everything and say that the gameplay should stop
	public static void EndGamePlay ()
	{
		isFinished = true;
		isPlaying = false;
	}

	//! \todo TO BE IMPLEMENTED
	public static void ShowResult ()
	{

	}
}
