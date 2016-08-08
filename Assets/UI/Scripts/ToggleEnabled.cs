using UnityEngine;
using System.Collections;

/*!
 * \brief A script which toggles the isActive of the __gameObject__ it is attached to on and off per a given interval.
 */
public class ToggleEnabled : MonoBehaviour
{
    public float interval;  //!< the intervat at which we are going to toggle.

    //! InvokeRepeating("Toggle", interval, interval)
    void Start()
    {
        InvokeRepeating("Toggle", interval, interval);
    }

    //!< gameObject.SetActive(!gameObject.activeInHierarchy)
    void Toggle()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
