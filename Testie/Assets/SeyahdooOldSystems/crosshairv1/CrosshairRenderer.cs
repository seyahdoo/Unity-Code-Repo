using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// sets Canvas's Camera to MainCamera for camera space resizing
/// </summary>
public class CrosshairRenderer : MonoBehaviour {


    void Awake()
    {

        Canvas canvas = GetComponent<Canvas>();
        if (canvas)
        {
            canvas.worldCamera = Camera.main;
            canvas.planeDistance = Camera.main.nearClipPlane + .01f;
        }

    }




}
