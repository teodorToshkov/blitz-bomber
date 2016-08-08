using UnityEngine;
using System.Collections;

/*!
 * \brief A script which Sets the text of the __ThreeDText__ compontent of the __GameObject__ to the _highscore_ __PlayerPref__
 */
public class HighScore : MonoBehaviour
{
    void Start()
    {
        (GetComponent<UI.ThreeDText>() as UI.ThreeDText).SetText(PlayerPrefs.GetInt("highscore", 0).ToString());
    }
}
