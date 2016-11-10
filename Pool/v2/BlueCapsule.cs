///Author: seyahdoo(Seyyid Ahmed Doğan) 
///github: github.com/seyahdoo
///email : contact@seyahdoo.com
///
///see my other reusable unity codes at
///https://github.com/seyahdoo/Unity-Code-Repo

using UnityEngine;
using System.Collections;

public class BlueCapsule : MonoBehaviour {
	
	void Update () {

		if (transform.position.y < 0) {

			this.Release();
		}

	}
}
