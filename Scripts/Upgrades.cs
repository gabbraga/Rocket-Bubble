using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
	public Text totalXP;
	public AudioClip buttonClick;
	private AudioSource source;
	private string soundKey = "isSoundOn";
	private int soundOn;


	// PlayerPref Constants
	private string xpKey = "Experience";
	private string healthKey = "totalHealth";
	private string fuelKey = "totalFuel";
	private string healthRestoreKey = "healthRestore";
	private string fuelRestoreKey = "fuelRestore";
	private string bubbleKey = "bubbleDuration";
	private float cost1 = 200f;
	private float cost2 = 300f;
	private float cost3 = 600f;
	public Image[] guages;



	void Awake ()
	{
		source = GetComponent<AudioSource> ();
		soundOn = PlayerPrefs.GetInt (soundKey, 1);

		// reset upgrades
		/*PlayerPrefs.SetFloat (healthKey, 100f);
		PlayerPrefs.SetFloat (fuelKey, 100f);
		PlayerPrefs.SetFloat (healthRestoreKey, 20f);
		PlayerPrefs.SetFloat (fuelRestoreKey, 30f);
		PlayerPrefs.SetFloat (bubbleKey, 5f);
		PlayerPrefs.SetFloat (xpKey, 3000f);*/



	}

	void Start ()
	{
		// set current values
		guages [0].fillAmount = (PlayerPrefs.GetFloat (healthKey, 100f) - 100f) / 100f;
		guages [1].fillAmount = (PlayerPrefs.GetFloat (fuelKey, 100f) - 100f) / 100f;
		guages [2].fillAmount = (PlayerPrefs.GetFloat (healthRestoreKey, 20f) - 20f) / 25f;
		guages [3].fillAmount = (PlayerPrefs.GetFloat (fuelRestoreKey, 30f) - 30f) / 25f;
		guages [4].fillAmount = (PlayerPrefs.GetFloat (bubbleKey, 5f) - 5f) / 10f;
	}

	void Update ()
	{
		totalXP.text = "XP " + PlayerPrefs.GetFloat (xpKey, 0f);
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
	 
	//formula for fill amount: current value - min value / difference between max and min

	public void HealthBtnClicked ()
	{
		if (PlayerPrefs.GetFloat (healthKey, 100f) < 200f && PlayerPrefs.GetFloat (xpKey, 0f) >= cost2) {
			PlayerPrefs.SetFloat (healthKey, PlayerPrefs.GetFloat (healthKey, 100f) + 20f);
			PlayerPrefs.SetFloat (xpKey, PlayerPrefs.GetFloat (xpKey) - cost2);
			guages [0].fillAmount = (PlayerPrefs.GetFloat (healthKey, 100f) - 100f) / 100f;
		}
	}

	public void FuelBtnClicked ()
	{
		if (PlayerPrefs.GetFloat (fuelKey, 100f) < 200f && PlayerPrefs.GetFloat (xpKey, 0f) >= cost2) {
			PlayerPrefs.SetFloat (fuelKey, PlayerPrefs.GetFloat (fuelKey, 100f) + 20f);
			PlayerPrefs.SetFloat (xpKey, PlayerPrefs.GetFloat (xpKey) - cost2);
			guages [1].fillAmount = (PlayerPrefs.GetFloat (fuelKey, 100f) - 100f) / 100f;
		}
	}

	public void HealthRestoreBtnClicked ()
	{
		if (PlayerPrefs.GetFloat (healthRestoreKey, 20f) < 45f && PlayerPrefs.GetFloat (xpKey, 0f) >= cost1) {
			PlayerPrefs.SetFloat (healthRestoreKey, PlayerPrefs.GetFloat (healthRestoreKey, 20f) + 5f);
			PlayerPrefs.SetFloat (xpKey, PlayerPrefs.GetFloat (xpKey) - cost1);
			guages [2].fillAmount = (PlayerPrefs.GetFloat (healthRestoreKey, 20f) - 20f) / 25f;
		}
	}

	public void FuelRestoreBtnClicked ()
	{
		if (PlayerPrefs.GetFloat (fuelRestoreKey, 30f) < 55f && PlayerPrefs.GetFloat (xpKey, 0f) >= cost1) {
			PlayerPrefs.SetFloat (fuelRestoreKey, PlayerPrefs.GetFloat (fuelRestoreKey, 30f) + 5f);
			PlayerPrefs.SetFloat (xpKey, PlayerPrefs.GetFloat (xpKey) - cost1);
			guages [3].fillAmount = (PlayerPrefs.GetFloat (fuelRestoreKey, 30f) - 30f) / 25f;
		}
	}

	public void ShieldDurationBtnClicked ()
	{
		if (PlayerPrefs.GetFloat (bubbleKey, 5f) < 15f && PlayerPrefs.GetFloat (xpKey, 0f) >= cost3) {
			PlayerPrefs.SetFloat (bubbleKey, PlayerPrefs.GetFloat (bubbleKey, 5f) + 2f);
			PlayerPrefs.SetFloat (xpKey, PlayerPrefs.GetFloat (xpKey) - cost3);
			guages [4].fillAmount = (PlayerPrefs.GetFloat (bubbleKey, 5f) - 5f) / 10f;
		}
	}
}
