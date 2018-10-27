using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PoolBenchmark : MonoBehaviour {

	// Use this for initialization
	void Start () {
        BencmarkCachedPool();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private static void Benchmark(Action act, int iterations)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        act.Invoke(); // run once outside of loop to avoid initialization costs
        DateTime start = DateTime.Now;
        for (int i = 0; i < iterations; i++)
        {
            act.Invoke();
        }
        DateTime end = DateTime.Now;
        UnityEngine.Debug.Log((end-start).Ticks / iterations);
    }

    Stack<RandomSpinningCube> cubes = new Stack<RandomSpinningCube>();

    #region bencmark cached
    void BencmarkCachedPool()
    {
        seyahdoo.pooling.v3.Pool.CreatePool<RandomSpinningCube>(null,20000,20000);

        Benchmark(() => { cubes.Push(seyahdoo.pooling.v3.Pool.Spawn<RandomSpinningCube>()); }, 10000);
        Benchmark(() => { seyahdoo.pooling.v3.Pool.Recover<RandomSpinningCube>(cubes.Pop()); }, 10000);
        Benchmark(() => { seyahdoo.pooling.v3.Pool.RecoverAll<RandomSpinningCube>(); }, 1);

        cubes.Clear();


    }

    float GetTestCached()
    {
        

        return 1f;
    }

    float ReleaseTestCached()
    {
        return 1f;
    }

    float ReleaseAllTestCached()
    {
        return 1f;
    }
    #endregion

    #region benchmark uncached
    void BencmarkUnCachedPool()
    {


    }


    float GetTestUnCached()
    {
        return 1f;
    }

    float ReleaseTestUnCached()
    {
        return 1f;
    }

    float ReleaseAllTestUnCached()
    {
        return 1f;
    }
    #endregion
}
