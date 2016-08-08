using UnityEngine;
using System.Collections;
using UnityEngine.Events;

/*!
 * \brief Invokes different UnityEvent's upon change in the state of the __Everyplay.IsSupported()__,
 * __Everyplay.IsRecordingSupported() and __Everyplay.IsRecording()__.
 */
public class OnEveryPlaySupported : MonoBehaviour
{
    public UnityEvent onIsSupported;                //!< a UnityEvent which controls what happens when EveryPlay is supported.
    public UnityEvent onIsNotSupported;             //!< a UnityEvent which controls what happens when EveryPlay is not supported.
    public UnityEvent onRecordingIsSupported;       //!< a UnityEvent which controls what happens when the recording is supported.
    public UnityEvent onRecordingIsNotSupported;    //!< a UnityEvent which controls what happens when the recording is __not__ supported.
    public UnityEvent onIsRecording;                //!< a UnityEvent which controls what happens when EveryPlay is recording.
    public UnityEvent onIsNotRecording;             //!< a UnityEvent which controls what happens when EveryPlay is __not__ recording.

    private bool isSupported = false;               //!< a local variable to hold the status of whether EveryPlay is supported.
    private bool isRecordingSupported = false;      //!< a local variable to hold the status of whether recording with EveryPlay is supported.
    private bool isRecording = false;               //!< a local variable to hold the status of whether EveryPlay is recording.

    //!< We set the initial state of isSupported and isRecordingSupported and invoke the appropriate UnityEvent's.
    void Start()
    {
        isSupported = Everyplay.IsSupported();
        if (isSupported) onIsSupported.Invoke();
        else onIsNotSupported.Invoke();

        isRecordingSupported = Everyplay.IsRecordingSupported();
        if (isRecordingSupported) onRecordingIsSupported.Invoke();
        else onRecordingIsNotSupported.Invoke();

        isRecording = Everyplay.IsRecording();
        if (isRecording) onIsRecording.Invoke();
        else onIsNotRecording.Invoke();
    }

    //!< checks if there is a change in the state of __Everyplay.IsSupported()__, __EveryPlay.IsRecording()__ and __Everyplay.IsRecordingSupported()__ and invokes the appropriate UnitEvent's
    void FixedUpdate()
    {
        if (isSupported != Everyplay.IsSupported())
        {
            isSupported = Everyplay.IsSupported();
            if (isSupported) onIsSupported.Invoke();
            else onIsNotSupported.Invoke();
        }
        if (isRecordingSupported != Everyplay.IsRecordingSupported())
        {
            isRecordingSupported = Everyplay.IsRecordingSupported();
            if (isRecordingSupported) onRecordingIsSupported.Invoke();
            else onRecordingIsNotSupported.Invoke();
        }
        if (isRecording != Everyplay.IsRecording())
        {
            isRecording = Everyplay.IsRecording();
            if (isRecording) onIsRecording.Invoke();
            else onIsNotRecording.Invoke();
        }
    }
}
