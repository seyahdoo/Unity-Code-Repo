using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataUpdator : MonoBehaviour {

	public Data exampleData;

	void Update(){

		if (exampleData) {
			exampleData.SetValue (Time.time.ToString ());
		}
	
	}

}
