using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class Main : MonoBehaviour
{

	//AUDIO STUFF
	public AudioClip buttonClick;
	private AudioSource source;
	private string musicKey = "isMusicOn";
	private string soundKey = "isSoundOn";
	private int musicVal;
	private int soundVal;


	void Awake ()
	{
		Screen.SetResolution (500, 668, false);

		source = GetComponent<AudioSource> ();

		musicVal = PlayerPrefs.GetInt (musicKey, 1);
		soundVal = PlayerPrefs.GetInt (soundKey, 1);
		if (musicVal == 1) {
			GameObject.Find ("Music Player").GetComponent <AudioSource> ().Play ();
		}
		//Debug.Log (musicVal.ToString ());
	}

	public void PlayBtnClicked ()
	{
		GameObject.Find ("Music Player").GetComponent<AudioSource> ().Stop ();
		StartCoroutine (wait ("GameplayScreen"));
	}

	public void InsBtnClicked ()
	{
		StartCoroutine (wait ("InstructionsScreen"));
	}

	public void SetBtnClicked ()
	{
		StartCoroutine (wait ("SettingsScreen"));
	}

	public void StoreBtnClicked ()
	{
		StartCoroutine (wait ("StoreScreen"));
	}


	IEnumerator wait (string scene)
	{
		int soundOn = PlayerPrefs.GetInt (soundKey, 1);
		if (soundOn == 1) {
			source.PlayOneShot (buttonClick, 1F);	
		}
		yield return new WaitForSeconds (source.clip.length);
		SceneManager.LoadScene (scene);
	}
}
