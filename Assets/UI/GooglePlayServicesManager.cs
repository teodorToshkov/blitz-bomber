using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine;
using System.Collections;

public class GooglePlayServicesManager : MonoBehaviour
{	
	// Use this for initialization
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

	private void SignInCallback(bool success)
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
}
