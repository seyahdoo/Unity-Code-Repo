using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using seyahdoo.pooling.v3;
using UnityEngine.SceneManagement;

public class pooltest : MonoBehaviour {

    public GameObject OriginalPrefab;

    public Stack<RandomSpinningCube> CubesInUse = new Stack<RandomSpinningCube>();

    public RandomSpinningCube cube;

	// Use this for initialization
	void Awake () {

        Pool.CreatePool<RandomSpinningCube>(OriginalPrefab);

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.G))
        {
            cube = Pool.Spawn<RandomSpinningCube>();

            CubesInUse.Push(cube);

            cube.transform.position = Vector3.right * 5;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            cube = CubesInUse.Pop();

            Pool.Recover<RandomSpinningCube>(cube);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Pool.RecoverAll<RandomSpinningCube>();
            CubesInUse.Clear();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Pool.Recover<RandomSpinningCube>(cube);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("pooltest_other_scene");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Pool.DestroyPool<RandomSpinningCube>();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            Pool.DestroyAllPools();
        }


    }
}
