using UnityEngine;
using System.Collections;

/*! \brief This script is responsible for changing a __GameObject__'s position.
 */
public class ChangeBackground : MonoBehaviour
{
    public Interpolate.AnimationType type;  //!< The type of animation we will use
    public float duration;      //!< The duration of the animation

    private float startTime;        //!< The time at which the animation starts

    private Color startBottomColor;  //!< The Color at the bottom at which the animation starts
    private Color currentBottomColor;//!< A holder for the current position, which we can manipulate more easily
    private Color targetBottomColor; //!< The position which we are aiming at

    private Color startTopColor;  //!< The position at which the animation starts
    private Color currentTopColor;//!< A holder for the current position, which we can manipulate more easily
    private Color targetTopColor; //!< The position which we are aiming at

    private float interval = 0.02f;	//!< The intervals of time we are going to update the position at

    private Material material;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        startBottomColor = material.GetColor("_Color");
        startTopColor = material.GetColor("_Color2");

        currentBottomColor = material.GetColor("_Color");
        currentTopColor = material.GetColor("_Color2");

        targetBottomColor = material.GetColor("_Color");
        targetTopColor = material.GetColor("_Color2");
    }
    
    public void ChangeTo(Color bottomColor, Color topColor)
    {
        targetBottomColor = bottomColor;
        targetTopColor = topColor;

        startTime = Time.time;
        startBottomColor = material.GetColor("_Color");
        startTopColor = material.GetColor("_Color2");
        LoopAnimation();
    }
    /// <summary>
    /// Loops the animation.
    /// </summary>
    void LoopAnimation()
    {
        if (Time.time < startTime + duration)
        {
            switch (this.type)
            {
                case Interpolate.AnimationType.linear:
                    currentBottomColor = Interpolate.Linear(startBottomColor, targetBottomColor, Time.time - startTime, duration);
                    currentTopColor = Interpolate.Linear(startTopColor, targetTopColor, Time.time - startTime, duration);
                    break;
                case Interpolate.AnimationType.easeIn:
                    currentBottomColor = Interpolate.EaseInQuad(startBottomColor, targetBottomColor, Time.time - startTime, duration);
                    currentTopColor = Interpolate.EaseInQuad(startTopColor, targetTopColor, Time.time - startTime, duration);
                    break;
                case Interpolate.AnimationType.easeOut:
                    currentBottomColor = Interpolate.EaseOutQuad(startBottomColor, targetBottomColor, Time.time - startTime, duration);
                    currentTopColor = Interpolate.EaseOutQuad(startTopColor, targetTopColor, Time.time - startTime, duration);
                    break;
                case Interpolate.AnimationType.easeInOut:
                    currentBottomColor = Interpolate.EaseInOutQuad(startBottomColor, targetBottomColor, Time.time - startTime, duration);
                    currentTopColor = Interpolate.EaseInOutQuad(startTopColor, targetTopColor, Time.time - startTime, duration);
                    break;
            }

            material.SetColor("_Color", currentBottomColor);
            material.SetColor("_Color2", currentTopColor);
            Invoke("LoopAnimation", interval);
        }
        else
        {
            material.SetColor("_Color", targetBottomColor);
            material.SetColor("_Color2", targetTopColor);
        }
    }
}
