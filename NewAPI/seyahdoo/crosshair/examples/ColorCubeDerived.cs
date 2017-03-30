using seyahdoo.crosshair;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace seyahdoo.crosshair.examples
{
    /// <summary>
    /// Simple Crosshair Aware cube, will go green if stared by Crosshair
    /// </summary>
    [RequireComponent (typeof (MeshRenderer))]
    public class ColorCubeDerived : CrosshairAware
    {

        Material m;

        void Awake()
        {
            m = GetComponent<MeshRenderer>().material;
        }

        protected override void FocusOn()
        {
            m.color = Color.green;
        }
        protected override void FocusOff()
        {
            m.color = Color.red;
        }


    }



}
