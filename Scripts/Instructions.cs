using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{

	//AUDIO STUFF
	public AudioClip buttonClick;
	private AudioSource source;
	private string soundKey = "isSoundOn";
	private int soundOn;

	void Awake ()
	{
		source = GetComponent<AudioSource> ();
		soundOn = PlayerPrefs.GetInt (soundKey, 1);
	}

	public void BackBtnClicked ()
	{
		StartCoroutine (wait ("HomeScreen"));
	}

	IEnumerator wait (string scene)
	{
		if (soundOn == 1) {
			source.PlayOneShot (buttonClick, 1F);	
		}
		yield return new WaitForSeconds (source.clip.length);
		SceneManager.LoadScene (scene);
	}
}