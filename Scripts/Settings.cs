using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

	public Text highScore;
	private string highScoreKey = "HighScore";
	private string musicKey = "isMusicOn";
	private string soundKey = "isSoundOn";
	private int soundOn, musicOn;
	public AudioClip buttonClick;
	private AudioSource source;
	private GameObject panel;
	public Toggle musicToggle;
	public Toggle soundToggle;

	void Awake ()
	{
		soundOn = PlayerPrefs.GetInt (soundKey, 1);
		musicOn = PlayerPrefs.GetInt (musicKey, 1);
		source = GetComponent<AudioSource> ();
		panel = GameObject.Find ("Panel");
		panel.SetActive (false);
		highScore.text = "";

	}

	// Use this for initialization
	void Start ()
	{
		highScore.text = "" + PlayerPrefs.GetFloat (highScoreKey, 0);
		if (musicOn == 0) {
			musicToggle.isOn = false;
		}
		if (soundOn == 0) {
			soundToggle.isOn = false;
		}
	}

	public void BackBtnClicked ()
	{
		StartCoroutine (wait ("HomeScreen"));
	}

	public void ResetBtnClicked ()
	{
		if (soundOn == 1) {
			source.PlayOneShot (buttonClick, 1F);
		}
		panel.SetActive (true);

	}

	public void YesBtnClicked ()
	{
		if (soundOn == 1) {
			source.PlayOneShot (buttonClick, 1F);
		}
		PlayerPrefs.SetFloat (highScoreKey, 0);
		panel.SetActive (false);
		highScore.text = "" + PlayerPrefs.GetFloat (highScoreKey, 0);
	}

	public void NoBtnClicked ()
	{
		if (soundOn == 1) {
			source.PlayOneShot (buttonClick, 1F);
		}
		panel.SetActive (false);
	}

	public void backgroundMusicToggled ()
	{
		if (musicToggle.isOn) {
			PlayerPrefs.SetInt (musicKey, 1);
			musicOn = 1;
			MusicPlayer.mpInstance.GetComponent <AudioSource> ().Play ();
		} else {
			PlayerPrefs.SetInt (musicKey, 0);
			musicOn = 0;
			MusicPlayer.mpInstance.GetComponent <AudioSource> ().Stop ();
		}
		PlayerPrefs.Save ();

	}

	public void soundsToggled ()
	{
		if (soundToggle.isOn) {
			PlayerPrefs.SetInt (soundKey, 1);
			soundOn = 1;
			source.PlayOneShot (buttonClick, 1F);	
		} else {
			PlayerPrefs.SetInt (soundKey, 0);
			soundOn = 0;
		}
		PlayerPrefs.Save ();
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
