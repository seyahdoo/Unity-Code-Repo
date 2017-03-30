using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace seyahdoo.crosshair.examples
{

    /// <summary>
    /// Crosshair example cube script, will go blue if stared by crosshair
    /// </summary>
    [RequireComponent (typeof (CrosshairAware))]
    [RequireComponent (typeof (MeshRenderer))]
    public class ColorChangingCube : MonoBehaviour
    {

        private CrosshairAware ca;
        private Material m;

        void Awake()
        {

            m = GetComponent<MeshRenderer>().material;
            ca = GetComponent<CrosshairAware>();

            ca.FocusOnEvent += ColorBlue;
            ca.FocusOffEvent += ColorRed;
        }

        void ColorBlue()
        {
            m.color = Color.blue;
        }

        void ColorRed()
        {
            m.color = Color.red;
        }

    }


}



