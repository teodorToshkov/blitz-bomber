using UnityEngine;
using System.Collections;

/*! \brief This script Spawns an instance of an object when the screen is touched/clicked.
 * 
 * It also has some timeout before it can spawn again after spawnig an instance of the given GameOnject
 */
public class OnClickSpawner : MonoBehaviour
{
	public GameObject target; //!< the GameObject we are going to spawn
	public Vector3 offset; //!< the offset of the spawner's position the spawnee's initial position will be
	public float rechargerTime; //!< the time that has to pass before we can spawn again

	private float timeoutTime = 0; //!< holds information about when it is time to spawn

	void Update ()
	{
		// If there is still time until we can next spawn, we decrease it if the gameplay is going
		if (timeoutTime > float.Epsilon)
		{
			timeoutTime -= Time.deltaTime;
		}
		// If the screen is touched/clicked and we are allowed to, we spawn
		if (Input.GetMouseButtonDown (0))
		{
			// If the gameplay is not playing, we resume it without spawning anything
			if (!GameManager.isPlaying)
				GameManager.ResumeGamePlay ();
			// If the gameplay is going, we spawn our object and set a timeout for when we can next spawn
			else if (timeoutTime <= float.Epsilon)
			{
				// We spawn the object and set the time that has to pass until we can next spawn to
				// the timeout devided by the rate at which the gameplay is running
				GameObject.Instantiate (target, transform.position + offset, Quaternion.identity);
				timeoutTime = rechargerTime / GameManager.gamePlaySpeed;
			}
		}
	}
}
