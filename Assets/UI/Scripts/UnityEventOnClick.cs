using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[RequireComponent(typeof (Collider))]
public class UnityEventOnClick : MonoBehaviour
{
    public UnityEvent doOnClick;      //!< a delegate regarding what should happen when the positions to spawn at increases

    // Use this for initialization
    void OnMouseUp ()
    {
        Debug.Log(gameObject.name + " was pressed");
        doOnClick.Invoke();
	}
}
