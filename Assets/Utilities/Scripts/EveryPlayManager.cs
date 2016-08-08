using UnityEngine;
using System.Collections;
using UnityEngine.Events;

/*!
 * \brief A script which provides EveryPlay functionality.
 */
public class EveryPlayManager : MonoBehaviour
{
    public UnityEvent onReadyForRecording;  //!< What to happen when the device is ready for recording?

    /// <summary>
    /// Calls the onReadyForRecording if it is enabled
    /// </summary>
    /// <param name="enabled">is the recording supported or not</param>
    public void OnReadyForRecording(bool enabled)
    {
        if (enabled)
        {
            // The recording is supported
            onReadyForRecording.Invoke();
        }
    }

    //! Adds the OnReadyForRecording to the __Everyplay.ReadyForRecording__ delegate
    void Start()
    {
        // Register for the Everyplay ReadyForRecording event
        Everyplay.ReadyForRecording += OnReadyForRecording;
    }

    //! Shows the EveryPlay UI
    public void OpenEveryPlay()
    {
        Everyplay.ShowWithPath("/feed/game");
    }

    //! Starts recording
    public void StartRecording()
    {
        Everyplay.StartRecording();
    }

    //! Pauses the recording
    public void PauseRecording()
    {
        Everyplay.PauseRecording();
    }

    //! Resumes the recording
    public void ResumeRecording()
    {
        Everyplay.ResumeRecording();
    }

    //! Stops the recording
    public void StopRecording()
    {
        Everyplay.StopRecording();
    }

    //! Plays the last recording
    public void PlayLastRecording()
    {
        Everyplay.PlayLastRecording();
    }
}
