///Author: seyahdoo(Seyyid Ahmed Doğan) 
///github: github.com/seyahdoo
///email : contact@seyahdoo.com
///
///see my other reusable unity codes at
///https://github.com/seyahdoo/Unity-Code-Repo

using UnityEngine;
using UnityEditor;
using seyahdoo.fading.singleton;


/// <summary>
/// A custom inspector script for fancy controll buttons
/// </summary>
[CustomEditor(typeof(FaderSingleton))]
public class FaderSingletonInspector : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FaderSingleton fader = (FaderSingleton)target;

        if (GUILayout.Button("FadeIn"))
        {
            fader.FadeIn();
        }

        if (GUILayout.Button("FadeOut"))
        {
            fader.FadeOut();
        }


    }
}