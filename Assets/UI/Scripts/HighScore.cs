using UnityEngine;
using System.Collections;

public class HighScore : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        (GetComponent<UI.ThreeDText>() as UI.ThreeDText).SetText(PlayerPrefs.GetInt("highscore", 0).ToString());
    }
}
