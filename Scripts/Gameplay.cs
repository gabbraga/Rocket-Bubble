using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Gameplay : MonoBehaviour
{
	//visible in editor

	public Text distance;
	public Text highScoreLabel;
	public float distanceScore = 0;
	public GameObject[] prefabEnemies;
	public GameObject[] prefabPowerups;
	public Image bubbleShieldOn;

	static public Gameplay S;

	private float highScore = 0;
	private string highScoreKey = "HighScore";
	private string musicKey = "isMusicOn";
	private float upperObstaclePadding = 0.5f;
	private float lowerObstaclePadding = 6f;
	private float distanceFactor;
	private float enemySpawnRate;
	private float powerupSpawnRate;
	public bool _paused;

	void Awake ()
	{
		Screen.SetResolution (500, 668, false);
		S = this;
		if (PlayerPrefs.GetInt (musicKey) == 0) {
			GameMusic.gmInstance.GetComponent <AudioSource> ().Stop ();
		}
		Utilities.SetCameraBounds (this.GetComponent<Camera> ());
		enemySpawnRate = 2f;
		powerupSpawnRate = 5f;
		Invoke ("SpawnEnemy", 1f);
		Invoke ("SpawnPowerups", 3f); 
		distanceFactor = 0.01f;
		_paused = false;
	}

	void Start ()
	{
		highScore = PlayerPrefs.GetFloat (highScoreKey, 0);
		highScoreLabel.text = "High Score  " + highScore;
	}

	void Update ()
	{
		if (!_paused) {

			if (Input.GetKey ("space")) {
				distanceFactor = 0.05f;
			} else {
				distanceFactor = 0.01f;
			}
			distanceTraveled ();

			if (distanceScore > highScore) {
				highScore = distanceScore;
				PlayerPrefs.SetFloat (highScoreKey, highScore);
				PlayerPrefs.Save ();
				highScoreLabel.text = "High Score  " + highScore;
			}
		}

	}

	void OnDisable ()
	{
		if (distanceScore > highScore) {
			PlayerPrefs.SetFloat (highScoreKey, distanceScore);
			PlayerPrefs.Save ();
		}
	}

	public void SpawnEnemy ()
	{
		int direction = UnityEngine.Random.Range (0, 2);
		int index = UnityEngine.Random.Range (0, prefabEnemies.Length);
		GameObject enemy = Instantiate (prefabEnemies [index]) as GameObject;
		enemy.GetComponent<Obstacles> ().direction = direction;
		Vector3 pos = Vector3.zero;
		float yMin = Utilities.camBounds.min.y + lowerObstaclePadding;
		float yMax = Utilities.camBounds.max.y - upperObstaclePadding;
		pos.y = UnityEngine.Random.Range (yMin, yMax);

		if (direction == 1) {
			
			pos.x = Utilities.camBounds.min.x - upperObstaclePadding;

		} else {

			enemy.transform.rotation = Quaternion.Euler (0, 180, 0);
			pos.x = Utilities.camBounds.max.x + upperObstaclePadding;

		}
		enemy.transform.position = pos;
		Invoke ("SpawnEnemy", enemySpawnRate);                           
	}

	public void SpawnPowerups ()
	{
		int index = UnityEngine.Random.Range (0, prefabPowerups.Length);
		GameObject go = Instantiate (prefabPowerups [index]) as GameObject;
		Vector3 pos = Vector3.zero;
		float xMin = Utilities.camBounds.min.x + upperObstaclePadding;
		float xMax = Utilities.camBounds.max.x - upperObstaclePadding;
		pos.x = UnityEngine.Random.Range (xMin, xMax);
		pos.y = Utilities.camBounds.max.y + upperObstaclePadding;
		go.transform.position = pos;
		Invoke ("SpawnPowerups", powerupSpawnRate);                           
	}

	public void distanceTraveled ()
	{
		distanceScore += distanceFactor;

		distanceScore = Mathf.Round (distanceScore * 100f) / 100f;
		distance.text = "Distance  " + distanceScore;
	}

	public void StopGameplay ()
	{

		//StartCoroutine (waitForSound());

		_paused = !_paused;

		if (_paused) {
			Time.timeScale = 0;
			//BackgroundMusic.PauseBGMusic ();
		} else {
			Time.timeScale = 1;
			//BackgroundMusic.PlayBGMusic ();
		}
	}
}
