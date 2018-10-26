using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using seyahdoo.pooling.v3;

public class pooltest : MonoBehaviour {

    public GameObject origtest;

    public readonly Stack<RandomSpinningCube> cubes = new Stack<RandomSpinningCube>();

    public RandomSpinningCube cube;

	// Use this for initialization
	void Start () {

        //Pool.CreatePool<RandomSpinningCube>(origtest);

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.G))
        {
            cubes.Push(Pool.Get<RandomSpinningCube>());
            cubes.Peek().transform.position = Vector3.right * 5;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Pool.Release<RandomSpinningCube>(cubes.Pop());
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Pool.ReleaseAll<RandomSpinningCube>();
            cubes.Clear();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Pool.Release<RandomSpinningCube>(cube);
        }
    }
}
