using seyahdoo.crosshair;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace seyahdoo.crosshair.examples
{
    /// <summary>
    /// Simple Crosshair Target cube, will go green if stared by Crosshair
    /// </summary>
    [RequireComponent (typeof (MeshRenderer))]
    public class ColorCubeDerived : Target
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
