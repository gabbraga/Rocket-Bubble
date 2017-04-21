using UnityEngine;
using System.Collections;

public class Powerups : MonoBehaviour
{
	private float speed = 4f;

	public Bounds bounds;
	public Vector3 boundsCenterOffset;


	void Awake ()
	{

		InvokeRepeating ("CheckOffscreen", 0f, 2f);

	}

	void Update ()
	{
		if (Input.GetKey ("space")) {
			speed = 7f;
		} else {
			speed = 4f;
		}
		Move ();
	}

	public virtual void Move ()
	{
		Vector3 tempPos = pos;
		tempPos.y -= speed * Time.deltaTime;
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
		if (bounds.size == Vector3.zero) {
			bounds = Utilities.CombineBoundsOfChildren (this.gameObject);
			boundsCenterOffset = bounds.center - transform.position;
		}
		bounds.center = transform.position + boundsCenterOffset;
		Vector3 off = Utilities.ScreenBoundsCheck (bounds, BoundsTest.offScreen);
		if (off != Vector3.zero) {
			if (off.y < 0) {
				Destroy (this.gameObject);
			}
		}
	}

	void OnCollisionEnter (Collision coll)
	{
		GameObject other = coll.gameObject;
		switch (other.tag) {
		case "Player":
			Destroy (other);
			break;
		}
	}
}

