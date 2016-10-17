using UnityEngine;
using System.Collections;

public class SpawnTester : MonoBehaviour {

	public string ObjectName;

	void Awake () {
        //Pool.SetSetting("BlueCapsule", true, 10);
		InvokeRepeating ("Spawn", 1f, 1f);
	}


	void Spawn () {
		GameObject go = this.Get (ObjectName);
		go.transform.position = transform.position;
		go.transform.rotation = transform.rotation;
		go.GetComponent<Rigidbody> ().velocity = Vector3.zero;
        

    }
}

