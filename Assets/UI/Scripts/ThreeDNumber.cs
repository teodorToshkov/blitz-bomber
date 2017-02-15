using UnityEngine;
using System.Collections;

namespace UI
{
    /*! ThreeDNumber.cs
     * 
     * \brief This script controls a 3D text which represents a number.
     * 
     * It stores the GameObjects which represent the digits.
     * It controls the score we have in the gameplay.
     */
    public class ThreeDNumber : MonoBehaviour
    {
        public Transform parent; //!< the position of the parent is the at which the last digit stands
        public Vector3 offest; //!< the offset which the digits should have between each other
        public GameObject[] digits; //!< the models of the digits

        public Interpolate.AnimationType type;  //!< The type of animation we will use
        public float duration;          //!< The duration of the animation

        private static int staticNumber = 0; //!< stores the value of the number representing our score
        private int _number = -1; //!< the number in the object we are looking at

        private float startTime;        //!< The time at which the animation starts
        private int startPosition;  //!< The position at which the animation starts
        private int currentPosition;//!< A holder for the current position, which we can manipulate more easily
        private int targetPosition; //!< The position which we are aiming at

        private float interval = 0.02f;	//!< The intervals of time we are going to update the position at

        // Update is called once per frame
        /// <summary>
        /// We have the static number which can be manipulated through other scripts
        /// 	and we have to see if the number we have displayed differs or not.

        // Note: will be changed once the digits become animated

        /// If it differs, we destroy all digits that are displayed currently and add the appropriate ones.\n
        /// We loop throuhg each digit in the "_number" using a buffer, which we get the last digit of
        /// 	and devide by 10 to go to the next one until the number we are left with becomes 0.\n
        /// We get the GameObject of the appropriate digit (digits [bugffer % 10]) and istantiate it
        /// 	at the position which is equal to the "offset" we have set and make its parent our number
        /// 	so that we may delete it easily when we need to update our number again.
        /// </summary>
        void Update()
        {
            // If the number which is displayed is the same as the static number, we don't have to do anything
            if (_number == staticNumber) return;

            // We set the number displayed to our static one because we are about to update it
            _number = staticNumber;
            // We create a buffer which we are going to use for the devision so that we don't manipulate our "_number"
            int buffer = _number;
            int index = 0; // stores the index we are at (i.e. 0th means last digit)

            // We delete the children preset which are the digits we are currently displaying
            if (transform.childCount > 0)
            {
                while (index < transform.childCount && transform.GetChild(index) != null)
                {
                    Destroy(transform.GetChild(index).gameObject);
                    index++;
                }
            }
            // We reset the index because we have deleted everything and should start instantiating at index 0
            index = 0;
            while (buffer > 0 || index == 0)
            {
                Vector3 newPosition = offest * index;
                GameObject newDigit = GameObject.Instantiate(digits[buffer % 10]) as GameObject;
                newDigit.transform.SetParent(parent);
                newDigit.transform.localPosition = newPosition;
                buffer /= 10;
                index++;
            }
        }

        /// <summary>
        /// Increases the __staticNumber__ with __number__.
        /// </summary>
        /// <param name="number">Number to increase with.</param>
        public static void IncreaseWith(int number)
        {
            staticNumber += number;
        }

        /// <summary>
        /// Simply returns the __staticNumber__
        /// </summary>
        /// <returns>The number.</returns>
        public static int GetNumber()
        {
            return staticNumber;
        }

        /// <summary>
        /// Sets the number to be equal to 0
        /// </summary>
        public void _ResetNumber()
        {
            staticNumber = 0;
        }

        /// <summary>
        /// Sets the number to be equal to 0
        /// </summary>
        public static void ResetNumber()
        {
            staticNumber = 0;
        }

        /// <summary>
        /// Plays Animation until the number reachers the Plyer Prefference _highscore_
        /// </summary>
        public void PlayAnimationToHighScore()
        {
            PlayAnimation(PlayerPrefs.GetInt("highscore", 0));
        }

        /// <summary>
        /// Plays an animation with a given number.
        /// </summary>
        /// <param name="finalNumber">The number, which we are aiming at.</param>
        public void PlayAnimation(int finalNumber)
        {
            targetPosition = finalNumber;

            startTime = Time.time;
            startPosition = staticNumber;
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
                        currentPosition = (int)(Interpolate.Linear(startPosition, targetPosition, Time.time - startTime, duration));
                        break;
                    case Interpolate.AnimationType.easeIn:
                        currentPosition = (int)(Interpolate.EaseInQuad(startPosition, targetPosition, Time.time - startTime, duration));
                        break;
                    case Interpolate.AnimationType.easeOut:
                        currentPosition = (int)(Interpolate.EaseOutQuad(startPosition, targetPosition, Time.time - startTime, duration));
                        break;
                    case Interpolate.AnimationType.easeInOut:
                        currentPosition = (int)(Interpolate.EaseInOutQuad(startPosition, targetPosition, Time.time - startTime, duration));
                        break;
                }

                staticNumber = currentPosition;
                Invoke("LoopAnimation", interval);
            }
            else
            {
                staticNumber = targetPosition;
            }
        }
    }
}
