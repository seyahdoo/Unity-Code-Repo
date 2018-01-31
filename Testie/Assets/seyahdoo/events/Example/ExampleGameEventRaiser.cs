// ----------------------------------------------------------------------------
// Fast Unity Event System
//
// Written using Ryan Ripple's Awesome Unity Game Architecture Talk
//
// Author: Seyyid Ahmed Doğan (seyahdoo)
// Date:   31/01/18
// ----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using seyahdoo.events;

public class ExampleGameEventRaiser : MonoBehaviour {

	public GameEvent toberaised;

	void Start(){
		StartCoroutine (Raisor ());
	}

	IEnumerator Raisor(){

		while (true) {

			ExampleEventData d = new ExampleEventData();
			d.Data = "Hello";

			toberaised.Raise (d);

			yield return new WaitForSeconds (Random.Range (0f, 2f));

		}

	}


}
