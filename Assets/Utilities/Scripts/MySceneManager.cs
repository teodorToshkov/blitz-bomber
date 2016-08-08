using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/*!
 * \brief Manager for functionality in the scene, mostly used in <a href="https://docs.unity3d.com/Manual/UnityEvents.html">__UnityEvent's__</a>
 */
public class MySceneManager : MonoBehaviour
{
    public UnityEvent onLoad;   //!< What happens when the scene loads.

    //! Invokes the __onLoad__ UnityEvent.
    void Start()
    {
        onLoad.Invoke();
    }

    /// <summary>
    /// Loads a scene by a given name.
    /// </summary>
    /// <param name="scene">The name of the scene.</param>
    public void _LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
