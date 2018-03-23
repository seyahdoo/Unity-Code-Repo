using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataUser : MonoBehaviour {

	public Data exampleData;

	void Awake(){
		if(exampleData)
			exampleData.OnValueChanged += (data) => print("Change Cought: "+data);
	}


}

