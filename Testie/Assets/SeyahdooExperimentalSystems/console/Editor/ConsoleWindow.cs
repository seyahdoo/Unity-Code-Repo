using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR

/// <summary>
/// A small window for pool controll
/// </summary>
public class ConsoleWindow : EditorWindow
{

    public string stringX = "";

    [MenuItem("Tools/Seyahdoo/Console Controls")]
    static void Init()
    {
        EditorWindow window = GetWindow(typeof(ConsoleWindow));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Seyahdoo Console API");

        stringX = EditorGUILayout.TextField("x ->", stringX);
        if (GUILayout.Button("Console.Excecute(x)"))
        {
            
            seyahdoo.console.Console.Excecute(stringX);
        }

        //if (Selection.activeGameObject)
        //{
        //    if (GUILayout.Button("Pool.Release(Selected)"))
        //    {
        //
        //        List<GameObject> toRelease = new List<GameObject>(Selection.gameObjects);
        //
        //        foreach (var item in toRelease)
        //        {
        //            //item.Release();
        //        }
        //    }
        //}
        //else
        //{
        //    GUILayout.Label("Select objects to use Release object function");
        //}

        this.Repaint();
    }


}


#endif
