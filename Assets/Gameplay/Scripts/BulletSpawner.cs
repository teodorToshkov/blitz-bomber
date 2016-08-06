using UnityEngine;
using System.Collections;

/*! \brief This script Spawns an instance of an object when the screen is touched/clicked.
 * 
 * It also has some timeout before it can spawn again after spawnig an instance of the given GameOnject
 */
public class BulletSpawner : MonoBehaviour
{
    public GameObject target; //!< the GameObject we are going to spawn
    public Transform parent; //!< the GameObject we are going to set as a parent of the object
    public Vector3 offset; //!< the offset of the spawner's position the spawnee's initial position will be
    public float rechargerTime; //!< the time that has to pass before we can spawn again

    public float gamePlaySpeedEffectorFactor = 1f; //!< the factor by which the recharderTime is affected by GameManager.gamePlaySpeed

    private float timeoutTime = 0; //!< holds information about when it is time to spawn

    //! A function which spanws the __target__ at the __offset__ if the __timeoutTime__ has expired.
    public void Spawn()
    {
        if (timeoutTime <= float.Epsilon)
        {
            // We spawn the object and set the time that has to pass until we can next spawn to
            // the timeout devided by the rate at which the gameplay is running
            GameObject newBullet = GameObject.Instantiate(target, transform.position + offset, Quaternion.identity) as GameObject;
            newBullet.transform.SetParent(parent);
            timeoutTime = rechargerTime / (1 + (GameManager.gamePlaySpeed - 1) * gamePlaySpeedEffectorFactor);
        }
    }

    //! Decreases the __timeoutTime__ with the __deltaTime__ scaled by __GameManager.gamePlaySpeed__
    void Update()
    {
        // If there is still time until we can next spawn, we decrease it if the gameplay is going
        if (timeoutTime > float.Epsilon && GameManager.isPlaying)
            timeoutTime -= Time.deltaTime;
    }
}
