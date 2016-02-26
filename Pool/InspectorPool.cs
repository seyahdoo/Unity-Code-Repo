#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Pool))]
public class CustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Take out this if statement to set the value using setter when ever you change it in the inspector.
        // But then it gets called a couple of times when ever inspector updates
        // By having a button, you can control when the value goes through the setter and getter, your self.
        if (GUILayout.Button("Fix Name"))
        {
            if (target.GetType() == typeof(Pool))
            {
                Pool p = (Pool)target;
                p.ObjectName = p.Original.name;
                Debug.Log(p.ObjectName);
            }
        }
    }
}

#endif