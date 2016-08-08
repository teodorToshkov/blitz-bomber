using UnityEngine;
using System.Collections;

/*!
 * \brief A script which makes the GameObject it is attached to not be destroyed on load.
 */
public class DontDestroyOnLoad : MonoBehaviour
{
    public bool unique = true;  //!< Does the GameObject have to destroy other GameObject's with the same name?

    //! Calls DontDestroyOnLoad
    /*!
     * If there exists another GameObject with the same name, the __gameObject__ is destroyed.
     */
    void Start()
    {
        if (unique && GameObject.Find(this.name) != gameObject)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}
