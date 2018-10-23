using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using seyahdoo.pooling.v3;

public class pooltest : MonoBehaviour {

    public GameObject origtest;

    public RandomSpinningCube cube;

	// Use this for initialization
	void Start () {

        //Pool.CreatePool<RandomSpinningCube>(origtest);

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.G))
        {
            cube = Pool.Get<RandomSpinningCube>();
            cube.transform.position = Vector3.right * 5;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Pool.Release<RandomSpinningCube>(cube);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Pool.ReleaseAll<RandomSpinningCube>();
        }
    }
}
