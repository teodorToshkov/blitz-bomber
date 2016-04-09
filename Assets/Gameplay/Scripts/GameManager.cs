using UnityEngine;
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
 * 		
 * 
 * \todo Implemet the function ShowResult
 */
public class GameManager : MonoBehaviour
{
	public float gamePlayQuickenRate = 0.01f;
	public float maxGamePlaySpeed = 2f;
	public Floor[] _floorPrefabs;

	public static bool isPlaying;
	public static bool isFinished;
	public static float gamePlaySpeed;
	public static Dictionary<Floor.Type, Floor> floorPrefabs;
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

	public static Floor GetFloor (int index)
	{
		return floors [index];
	}

	public static Floor GetRandomFloor ()
	{
		int rand = Random.Range (0, floors.Count - 1);
		foreach (var floor in floors) {
			if (floor.gameObject.activeSelf && --rand <= 0)
				return floor;
		}
		return floors[0];
	}

	//! Adds a new floor to the list
	public static void AddFloor (Floor.Type floorType, Vector3 position)
	{
		foreach (Floor floor in floors)
		{
			if (!floor.gameObject.activeSelf && floor.type == floorType)
			{
				floor.gameObject.SetActive (true);
				floor.transform.position = position;
				return;
			}
		}
		Floor newFloor;
		if (floorPrefabs.TryGetValue (floorType, out newFloor))
		{
			GameObject newGameObj = GameObject.Instantiate (newFloor.gameObject, position, Quaternion.identity) as GameObject;
			floors.Add (newGameObj.GetComponent<Floor> ());
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
		floor.OnDestruction ();
		floor.gameObject.SetActive (false);
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
