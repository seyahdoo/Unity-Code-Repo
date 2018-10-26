using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ValidateTester : MonoBehaviour {

    [SerializeField]
    private Rigidbody rb;

    //strong way to never get a null reference.
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

#endif

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        rb.AddForce(Vector3.up * 10f);
	}
}
