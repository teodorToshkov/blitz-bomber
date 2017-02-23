using UnityEngine;
using System.Collections;

/*! \brief A script which slows everything down.
 * 
 * We do so using __time.TimeScale__ and not GameManager.gamePlaySpeed because we want to have the effect of slowing down everything
 * by the same factor.
 * 
 * We also make each next such Floor destroyed slow down the time less:
 * <code>Time.timeScale *= 1 - (slowDownRate / numberOfFloorsDestroyed)</code>
 * 
 * \todo Make the _time_ be initialized by __PlayerPrefs__ so that we may have a store where to upgrade our "special" __Floors__
 * 
 * \todo Make it call a function which indicates when a buff is "taken" and "lost". (Maybe a big icon which fades and enlarges).
 */
public class TimeSlowerFloorScript : Floor
{
	public float time = 1f;				//!< the time for which everything will be slowed down.
	public float slowDownRate = 0.5f;	//!< the rate at which everything will be slowed down.
    private ChangeBackground background;
    public Color startBottomColor;
    public Color startTopColor;
    public Color targetBottomColor;
    public Color targetTopColor;

    [HideInInspector]
    public AudioSource backgroundMusic;

    private static int numberOfFloorsDestroyed;	//!< the number of __Floors__ that are currently slowing the time down.

    void Start ()
    {
        backgroundMusic = GameObject.Find("Background Music").GetComponent<AudioSource>();
        background = GameObject.Find("Background").GetComponent<ChangeBackground>();
    }

	//! Holds information about what happents when the __Floor__ is destroyed, can be called from other scripts.
	/*!
	 * We set that we have destroyed the Floor and invoke the base Destroy method after a given time.\n
	 * We decrease the __Time.timeScale__ by the appropriate factor.\n
	 * Depending on the numberOfFloorsDestroyed we decrease by a different factor.\n
	 * <code>Time.timeScale *= 1 - (slowDownRate / numberOfFloorsDestroyed)</code>\n
	 * i.e. if we have a __slowDownRate__ equal to 0.5f and 1 _active_ __TimeSlowerFloorScript__'s and we Destroy another one,
	 * we will decrease the __Time.timeScale__ by a factor of _1 - (0.5f / 2) = 1 - 0.25f = 0.75f_.
	 * And therefore the overal __Time.timeScale__ will be _0.5f * 0.75f = 0.375f_.
	 */
	public override void Destroy ()
	{
		numberOfFloorsDestroyed = Mathf.Max (1, numberOfFloorsDestroyed + 1);

		GetComponent<Collider> ().enabled = false;
		GetComponent<Renderer> ().enabled = false;
		transform.GetChild(0).gameObject.SetActive (false);

		Time.timeScale *= 1 - (slowDownRate / numberOfFloorsDestroyed);

		destroyedFloor.gameObject.SetActive (true);

		// We increase the score of the player
		UI.ThreeDNumber.IncreaseWith (1);

		if (destroyedFloor != null)
		{
			DestroyedFloorInitializer destroyedFloorInitializer = (GameObject.Instantiate (destroyedFloor, transform.position, Quaternion.identity)
				as GameObject).GetComponent<DestroyedFloorInitializer> ();
			if (color != null)
				destroyedFloorInitializer.color = color;
			if (sprite != null)
				destroyedFloorInitializer.sprite = sprite;
		}

        background.ChangeTo(targetBottomColor, targetTopColor);

		Invoke ("Desactivate", time);

        backgroundMusic.pitch = Time.timeScale / 2 + 0.5f;
	}

	//! Desactivate this instance.
	/*! We call Floor.Destroy and decrease the __numberOfFloorsDestroyed__
	 * We revert the slowing of time that was made in the __Destroy__ method with the same logic.\n
	 * And we reactivate all its components, although the whole __GameObject__ itself is descativated by Floor.Destroy.
	 * We do this so that when we "respawn" the same Floor, it will be visible and work as supposed.
	 */
	void Desactivate ()
	{
		Time.timeScale /= 1 - (slowDownRate / numberOfFloorsDestroyed);

		numberOfFloorsDestroyed--;

		GetComponent<Collider> ().enabled = true;
		GetComponent<Renderer> ().enabled = true;
		transform.GetChild(0).gameObject.SetActive (true);

        background.ChangeTo(startBottomColor, startTopColor);

        gameObject.SetActive (false);

        backgroundMusic.pitch = Time.timeScale / 2 + 0.5f;
    }
}
