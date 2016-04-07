using UnityEngine;
using System.Collections;

/*! ThreeDNumber.cs
 * 
 * \brief This script controls a 3D text which represents a number.
 * 
 * It stores the GameObjects which represent the digits.
 * It controls the score we have in the gameplay.
 * 
 * \todo Change the simple __GameObject__ substitution with an animation transition
 * 		 using an __AnimatorController__ with animations:
 * 		 - 0 -> 1
 * 		 - 1 -> 2
 * 		 - 2 -> 3
 * 		 - 3 -> 4
 * 		 - 4 -> 5
 * 		 - 5 -> 6
 * 		 - 6 -> 7
 * 		 - 7 -> 8
 * 		 - 8 -> 9
 * 		 - 9 -> 0
 */
public class ThreeDNumber : MonoBehaviour
{
	public Vector3 position; //!< the position at which the last digit stands
	public Vector3 offest; //!< the offset which the digits should have between each other
	public GameObject[] digits; //!< the models of the digits

	private static int staticNumber = 0; //!< stores the value of the number representing our score
	private int _number = -1; //!< the number in the object we are looking at

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
	void Update ()
	{
		// If the number which is displayed is the same as the static number, we don't have to do anything
		if (_number == staticNumber) return;

		// We set the number displayed to our static one because we are about to update it
		_number = staticNumber;
		// We create a buffer which we are going to use for the devision so that we don't manipulate our "_number"
		int buffer = _number;
		int index = 0; // stores the index we are at (i.e. 0th means last digit)

		// We delete the children preset which are the digits we are currently displaying
		if(transform.childCount > 0)
		{
			while (index < transform.childCount && transform.GetChild (index) != null)
			{
				Destroy (transform.GetChild (index).gameObject);
				index++;
			}
		}
		// We reset the index because we have deleted everything and should start instantiating at index 0
		index = 0;
		while (buffer > 0 || index == 0)
		{
			Vector3 newPosition = position + offest * index;
			GameObject newDigit = GameObject.Instantiate (digits [buffer % 10], newPosition, Quaternion.identity) as GameObject;
			newDigit.transform.SetParent (transform);
			buffer /= 10;
			index++;
		}
	}

	/// <summary>
	/// Increases the __staticNumber__ with __number__.
	/// </summary>
	/// <param name="number">Number to increase with.</param>
	public static void IncreaseWith (int number)
	{
		staticNumber += number;
	}

	/// <summary>
	/// Simply returns the __staticNumber__
	/// </summary>
	/// <returns>The number.</returns>
	public static int GetNumber ()
	{
		return staticNumber;
	}

	/// <summary>
	/// Sets the number to be equal to 0
	/// </summary>
	public static void ResetNumber ()
	{
		staticNumber = 0;
	}
}
