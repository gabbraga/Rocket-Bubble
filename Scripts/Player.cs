using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	// PlayerPref Constants
	private string healthKey = "totalHealth";
	private string fuelKey = "totalFuel";
	private string healthRestoreKey = "healthRestore";
	private string fuelRestoreKey = "fuelRestore";
	private string bubbleKey = "bubbleDuration";
	private string xpKey = "Experience";

	//visible in Editor
	public GameObject panel;
	public float speed = 30;
	public Text awardedXP;
	public Text totalXP;
	public Image healthbar;
	public Image fuelbar;

	private Bounds bounds;
	private float birdDamage = 10f;
	private float eagleDamage = 20f;
	private float planeDamage = 30f;
	private float helicopterDamage = 40f;
	private float depleteRate = 1f;
	private Rigidbody2D rb;
	public GameObject bubbleShield;
	private float health;
	private float fuel;
	private float healthFill;
	private float fuelFill;
	private float bubbleDuration;

	//AUDIO CLIPS

	public AudioClip birdChirp;
	public AudioClip eagleCry;
	public AudioClip airplane;
	public AudioClip fuelBurst;
	public AudioClip powerup;
	public AudioClip bubblePop;
	public AudioClip fuelGlug;
	public AudioClip shipRepair;

	private AudioSource audioSource;
	private int soundVal;

	void Awake ()
	{			
		panel.SetActive (false);
		rb = this.GetComponent<Rigidbody2D> ();
		health = PlayerPrefs.GetFloat (healthKey, 100f);
		fuel = PlayerPrefs.GetFloat (fuelKey, 100f);
		healthFill = PlayerPrefs.GetFloat (healthRestoreKey, 20f);
		fuelFill = PlayerPrefs.GetFloat (fuelRestoreKey, 30f);
		bubbleDuration = PlayerPrefs.GetFloat (bubbleKey, 5f);
		bubbleShield.SetActive (false);
		audioSource = gameObject.GetComponent<AudioSource> ();
		soundVal = PlayerPrefs.GetInt ("isSoundOn", 1);

	}

	void Start ()
	{
		InvokeRepeating ("fuelDeplete", 0f, 0.5f);

	}

	void Update ()
	{
		// Pull in information from the Input class
		float xAxis = Input.GetAxis ("Horizontal");
		float yAxis = Input.GetAxis ("Vertical");

		// Change transform.position based on the axes
		Vector2 pos = transform.position;
		pos.x += xAxis * speed * Time.deltaTime;
		//pos.y += yAxis * speed * Time.deltaTime;
		transform.position = pos;

		bounds.center = transform.position;    

		if (rb.isKinematic == true) {
			// Keep the ship constrained to the screen bounds
			Vector2 off = Utilities.ScreenBoundsCheck (bounds, BoundsTest.onScreen);  
			if (off != Vector2.zero) {    
				pos -= off;
				transform.position = pos;
			}
		}


		transform.rotation = Quaternion.Euler (yAxis, xAxis, xAxis * -30);

		if (Input.GetKey ("space")) {
			depleteRate = 5f;
			//if (!audioSource.isPlaying) {
			//	audioSource.PlayOneShot (fuelBurst);
			//}
		} else {
			depleteRate = 1f; 
		}
		if ((healthbar.fillAmount <= 0) || fuelbar.fillAmount <= 0) {
			if (!Gameplay.S._paused) {
				Gameplay.S._paused = true;
				PlayerPrefs.SetFloat (xpKey, PlayerPrefs.GetFloat (xpKey, 0) + Mathf.Floor (Gameplay.S.distanceScore * 2f));
				awardedXP.text = "XP Awarded \t" + Mathf.Floor (Gameplay.S.distanceScore * 2f);
				totalXP.text = "Total XP \t\t" + PlayerPrefs.GetFloat (xpKey, 0);
				panel.SetActive (true);
			}

		}

	}


	void OnTriggerEnter2D (Collider2D other)
	{
		switch (other.gameObject.tag) {
		case "bird":
			if (soundVal == 1) {
				audioSource.PlayOneShot (birdChirp);
			}
			Destroy (other.gameObject);
			if (!bubbleShield.active) {
				healthbar.fillAmount -= (birdDamage / health);
			} else {
				if (soundVal == 1) {
					audioSource.PlayOneShot (bubblePop);
				}
				bubbleShield.SetActive (false);
			}
			break;

		
		case "eagle":
			if (soundVal == 1) {
				audioSource.PlayOneShot (eagleCry);
			}
			Destroy (other.gameObject);
			if (!bubbleShield.active) {
				healthbar.fillAmount -= (eagleDamage / health);
			} else {
				if (soundVal == 1) {
					audioSource.PlayOneShot (bubblePop);
				}
				bubbleShield.SetActive (false);
			}
			break;

		case "plane":
			Destroy (other.gameObject);
			if (!bubbleShield.active) {
				healthbar.fillAmount -= (planeDamage / health);
			} else {
				if (soundVal == 1) {
					audioSource.PlayOneShot (bubblePop);
				}
				bubbleShield.SetActive (false);
			}
			break;

		case "helicopter":
			Destroy (other.gameObject);
			if (!bubbleShield.active) {
				healthbar.fillAmount -= (helicopterDamage / health);
			} else {
				if (soundVal == 1) {
					audioSource.PlayOneShot (bubblePop);
				}
				bubbleShield.SetActive (false);
			}
			break;

		case "health":
			if (soundVal == 1) {
				audioSource.PlayOneShot (shipRepair, 1);
			}
			Destroy (other.gameObject);
			healthbar.fillAmount += healthFill / health;
			break;

		case "fuel":
			if (soundVal == 1) {
				audioSource.PlayOneShot (fuelGlug);
			}
			Destroy (other.gameObject);
			fuelbar.fillAmount += fuelFill / fuel;
			break;

		case "bubble":

			if (soundVal == 1) {
				audioSource.PlayOneShot (powerup);
			}
			Destroy (other.gameObject);
			if (!bubbleShield.active) {
				bubbleShield.SetActive (true);
				StartCoroutine (wait ());
			}
			break;
		}
	}

	void fuelDeplete ()
	{
		fuelbar.fillAmount = fuelbar.fillAmount - depleteRate / fuel;
	}

	public void PlayAgainClicked ()
	{
		Application.LoadLevel ("GameplayScreen");
	}

	public void MainMenuClicked ()
	{
		Application.LoadLevel ("HomeScreen");
	}

	IEnumerator wait ()
	{
		yield return new WaitForSeconds (PlayerPrefs.GetFloat (bubbleKey, 5f));
		if (soundVal == 1) {
			audioSource.PlayOneShot (bubblePop);
		}
		bubbleShield.SetActive (false);
	}
}
