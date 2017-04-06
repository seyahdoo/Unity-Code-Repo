using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// sets Canvas's Camera for camera space resizing
/// </summary>
public class CrosshairRenderer : MonoBehaviour {


    void Awake()
    {

        Canvas canvas = GetComponent<Canvas>();
        if (canvas)
        {
            canvas.worldCamera = Camera.allCameras[0];
            canvas.planeDistance = Camera.allCameras[0].nearClipPlane + .01f;
        }

    }




}
