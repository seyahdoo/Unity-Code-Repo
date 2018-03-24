using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeOtherScript : MonoBehaviour {

	public SomeManager someManager;

	void Awake(){
		InvokeRepeating ("UseManager", 0, 1);
	}


	void UseManager(){
	
		SomeManager.Instance ().logit (Time.time.ToString ());

		//someManager.logit (Time.time.ToString ());

	}

}
