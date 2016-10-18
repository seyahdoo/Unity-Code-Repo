using UnityEngine;
using System.Collections;

public class BlueCapsule : MonoBehaviour {
	
	void Update () {

		if (transform.position.y < 0) {

			this.Release();
		}

	}
}
