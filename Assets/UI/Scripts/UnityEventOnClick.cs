using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace UI
{
    [RequireComponent(typeof(Collider))]
/*! \brief Invokes a UnityEvent upon __OnMouseDown__
 * 
 * The user defines what happens when the GameObject, which the script is attached to, is pressed.
 */
    public class UnityEventOnClick : MonoBehaviour
    {
        public UnityEvent doOnClick;      //!< a delegate regarding what should happen when the positions to spawn at increases

        public float delay;
        public UnityEvent doOnClickWithDelay;      //!< a delegate regarding what should happen when the positions to spawn at increases

        //! Logs in the console that the gameObject was pressed and invokes the __doOnClick__ delegate
        void OnMouseDown()
        {
            Debug.Log(gameObject.name + " was pressed");
            doOnClick.Invoke();
            Invoke("DoOnClickWithDelay", delay);
        }

        void DoOnClickWithDelay()
        {
            doOnClickWithDelay.Invoke();
        }
    }
}
