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

public class ExampleEventUser :  GameEventUser {

	public override void OnEventInvoked (string methodname, object eventData){

		if (methodname == "OnDebugRequested") {
			OnDebugRequested (eventData);
		}

	}

	public void OnDebugRequested(object eventData){

		//Try to Get Data
		ExampleEventData mydata = null;
		if (eventData != null) {
			if (eventData.GetType() == typeof(ExampleEventData)) {
				mydata = (ExampleEventData)eventData;
			}
		}

		//Log Data if we got any data
		if (mydata != null) {
			Debug.Log (mydata.Data);
		}

		//Event Raised
		Debug.Log ("DEBUG!");
	}

}
