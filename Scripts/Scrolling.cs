using UnityEngine;
using System.Collections;

public class Scrolling : MonoBehaviour
{

	//public GameObject ship;
	public GameObject[] panels;
	public float scrollSpeed = -4f;
	public float sensitivity = 0.5f;

	private float panelHeight;
	private float depth;

	void Start ()
	{
		panelHeight = 10.15f;

		// Set initial positions of panels
		panels [0].transform.position = new Vector2 (0, 0);
		panels [1].transform.position = new Vector2 (0, panelHeight);
	}

	void Update ()
	{
		if (Input.GetKey ("space")) {
			scrollSpeed = -12f;
		} else {
			scrollSpeed = -8f;
		}

		float tY, tX = 0;
		tY = Time.time * scrollSpeed % panelHeight + (panelHeight);

		/*
		if (ship != null) {
			tX = -ship.transform.position.x * sensitivity;
		}*/

		panels [0].transform.position = new Vector2 (tX, tY - panelHeight);
		panels [1].transform.position = new Vector2 (tX, tY);



	}
}

