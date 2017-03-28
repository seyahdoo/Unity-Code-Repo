///Author: seyahdoo(Seyyid Ahmed Doğan) 
///github: github.com/seyahdoo
///email : contact@seyahdoo.com
///
///see my other reusable unity codes at
///https://github.com/seyahdoo/Unity-Code-Repo

using UnityEngine;
using UnityEditor;
using seyahdoo.fadevr;


/// <summary>
/// A custom inspector script for fancy controll buttons
/// </summary>
[CustomEditor(typeof(Fader))]
public class FaderInspector : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        if (GUILayout.Button("FadeIn"))
        {
            Fader.FadeIn();
        }

        if (GUILayout.Button("FadeOut"))
        {
            Fader.FadeOut();
        }


    }
}