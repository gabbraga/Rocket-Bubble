using UnityEngine;
using System.Collections;

public class Obstacles : MonoBehaviour
{
	//AUDIO STUFF
	private float speedHor = 6f;
	private float speedVer = 8f;
	public bool ________________;
	public Bounds bounds;
	public Vector3 boundsCenterOffset;
	public int direction;

	void Awake ()
	{
		InvokeRepeating ("CheckOffscreen", 0f, 2f);
	}

	void Update ()
	{
		if (Input.GetKey ("space")) {
			speedVer = 12f;
		} else {
			speedVer = 8f;
		}
		Move ();


	}

	public virtual void Move ()
	{
		Vector3 tempPos = pos;
		if (direction == 1) {
			tempPos.x += speedHor * Time.deltaTime;
		} else {
			tempPos.x -= speedHor * Time.deltaTime;
		}
		tempPos.y -= speedVer * Time.deltaTime;
		pos = tempPos;
	}

	// This is a Property: A method that acts like a field
	public Vector3 pos {
		get {
			return(this.transform.position);
		}
		set {
			this.transform.position = value;
		}
	}

	void CheckOffscreen ()
	{
		// If bounds are still their default value...
		if (bounds.size == Vector3.zero) {
			// then set them
			bounds = Utilities.CombineBoundsOfChildren (this.gameObject);
			// Also find the diff between bounds.center & transform.position
			boundsCenterOffset = bounds.center - transform.position;
		}

		// Every time, update the bounds to the current position
		bounds.center = transform.position + boundsCenterOffset;
		// Check to see whether the bounds are completely offscreen
		Vector3 off = Utilities.ScreenBoundsCheck (bounds, BoundsTest.offScreen);
		if (off != Vector3.zero) {
			// If this enemy has gone off the bottom edge of the screen
			if (off.y < 0) {
				// then destroy it
				//Gameplay.S.addToScreen (this);
				Destroy (this.gameObject);

			}
		}
	}



}
