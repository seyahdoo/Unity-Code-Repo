using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using seyahdoo.pooling.v3;

public class CubeSystem : MonoBehaviour {

    private void Awake()
    {

        Pool.CreatePool<RandomSpinningCube>();

        InvokeRepeating("SpawnCube", 0f, .5f);

        InvokeRepeating("DeSpawnCube", 4f, .5f);

    }

    public Queue<RandomSpinningCube> queue = new Queue<RandomSpinningCube>();

    void SpawnCube()
    {
        RandomSpinningCube cube = Pool.Get<RandomSpinningCube>();

        cube.transform.position = Random.insideUnitSphere * 4;

        queue.Enqueue(cube);

    }

    void DeSpawnCube()
    {
        queue.Dequeue().Release();

    }


}
