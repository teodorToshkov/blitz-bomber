using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine;
using System.Collections;

/*! \brief This script is the manager that is responsible for controlling the Google Play Services.
 */
public class GooglePlayServicesManager : MonoBehaviour
{	
	//! Initialization
	void Start ()
	{
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

		PlayGamesPlatform.InitializeInstance(config);
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();
		// Authenticate
		Debug.Log ("Authenticating...");

		PlayGamesPlatform.Instance.Authenticate(SignInCallback, PlayGamesPlatform.Instance.localUser.authenticated);
	}

	/// <summary>
	/// callback for signing in.
	/// </summary>
	/// <param name="success">If <c>true</c>, the signin has been successful.</param>
	private void SignInCallback (bool success)
	{
		if (success)
		{
			Debug.Log ("Signed In: " + PlayGamesPlatform.Instance.localUser.userName);
		}
		else
		{
			Debug.Log ("Sign-in failed.");
		}
	}

	/// <summary>
	/// Reports the score to the leaderboard.
	/// </summary>
	public static void ReportScore ()
	{
		Social.ReportScore (ThreeDNumber.GetNumber(), GPGSIds.leaderboard_score, (bool success) => {
			Debug.Log (PlayGamesPlatform.Instance.GetUserDisplayName() + " reporting a score of " + ThreeDNumber.GetNumber() + ": "
				+ (success ? "Successful!" : "Unsuccessful!"));
		});
	}
}
