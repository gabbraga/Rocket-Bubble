using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
	public static MusicPlayer mpInstance = null;
	private string muteMusicKey = "isMusicMuted";

	void Awake ()
	{
		if (mpInstance == null) {
			mpInstance = this;
			GameObject.DontDestroyOnLoad (mpInstance);
		} else {
			Destroy (gameObject);
		}
		//int value = PlayerPrefs.GetInt (muteMusicKey, 1);
		//if (value == 1) {
		//	GameObject.Find ("Music Player").GetComponent <AudioSource> ().Play ();
		//}
	}
}
