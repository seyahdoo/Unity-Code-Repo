#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PoolMember))]
public class InspectorPoolMember : Editor {


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Take out this if statement to set the value using setter when ever you change it in the inspector.
        // But then it gets called a couple of times when ever inspector updates
        // By having a button, you can control when the value goes through the setter and getter, your self.
        if (GUILayout.Button("Return Pool"))
        {
            if (target.GetType() == typeof(PoolMember))
            {
                PoolMember pm = (PoolMember)target;
                pm.ReturnPool();
                Debug.Log("Returned");
            }
        }
    }
}

[CustomEditor(typeof(PlayerController))]
public class InspectorPlayerController : Editor
{


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Take out this if statement to set the value using setter when ever you change it in the inspector.
        // But then it gets called a couple of times when ever inspector updates
        // By having a button, you can control when the value goes through the setter and getter, your self.
        if (GUILayout.Button("Stop"))
        {
            if (target.GetType() == typeof(PlayerController))
            {
                PlayerController player = (PlayerController)target;
                player.Stop();

                Debug.Log("Stopped");
            }
        }

        if (GUILayout.Button("Start"))
        {
            if (target.GetType() == typeof(PlayerController))
            {
                PlayerController player = (PlayerController)target;
                player.Start();

                Debug.Log("Started");
            }
        }

        

    }
}


#endif