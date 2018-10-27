using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using seyahdoo.pooling.v3;
using UnityEngine.SceneManagement;

public class PoolTester : MonoBehaviour {

    public GameObject OriginalPrefab;

    public Stack<RandomSpinningCube> CubesInUse = new Stack<RandomSpinningCube>();

    public RandomSpinningCube cube;
	
}

 #if UNITY_EDITOR
[CustomEditor(typeof(PoolTester))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PoolTester pt = (PoolTester)target;

        if (GUILayout.Button("Create Pool"))
        {
            Pool.CreatePool<RandomSpinningCube>(pt.OriginalPrefab);
        }

        if (GUILayout.Button("Spawn Object"))
        {
            RandomSpinningCube c;

            c = Pool.Spawn<RandomSpinningCube>();
            c.transform.rotation.SetLookRotation(Random.insideUnitSphere);

            pt.CubesInUse.Push(c);
        }

        if (GUILayout.Button("Recover Last Spawned Object"))
        {
            Pool.Recover<RandomSpinningCube>(pt.CubesInUse.Pop());
        }

        if (GUILayout.Button("Recover All"))
        {
            Pool.RecoverAll<RandomSpinningCube>();
        }

        if (GUILayout.Button("Recover Selected Object"))
        {
            Pool.Recover<RandomSpinningCube>(pt.cube);
        }

        if (GUILayout.Button("Load \"pooltest_other_scene\""))
        {
            SceneManager.LoadScene("pooltest_other_scene");
        }

        if (GUILayout.Button("Load \"pooltest\" scene"))
        {
            SceneManager.LoadScene("pooltest");
        }

        if (GUILayout.Button("Destroy Pool"))
        {
            Pool.DestroyPool<RandomSpinningCube>();
        }

        if (GUILayout.Button("Destroy All Pools"))
        {
            Pool.DestroyAllPools();
        }

        if (GUILayout.Button("Recover Object"))
        {
            Pool.Recover<RandomSpinningCube>(pt.CubesInUse.Pop());
        }

        if (GUILayout.Button("Recover Object"))
        {
            Pool.Recover<RandomSpinningCube>(pt.CubesInUse.Pop());
        }



    }
}
 #endif

