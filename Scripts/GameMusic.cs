using UnityEngine;
using System.Collections;

public class GameMusic : MonoBehaviour
{
	public static GameMusic gmInstance = null;

	void Awake ()
	{
		if (gmInstance == null) {
			gmInstance = this;
		} else {
			Destroy (gameObject);
		}
			
	}
}
