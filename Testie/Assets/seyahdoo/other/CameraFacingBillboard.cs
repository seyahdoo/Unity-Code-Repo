///Author: seyahdoo(Seyyid Ahmed Doğan) 
///github: github.com/seyahdoo
///email : contact@seyahdoo.com
///
///see my other reusable unity codes at
///https://github.com/seyahdoo/Unity-Code-Repo

///Changelog:
///XX.XX.XXXX -> Copied from internet, source: http://wiki.unity3d.com/index.php?title=CameraFacingBillboard

///usage:
///Attach to any 2d object

///Dependancies:
///None

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace seyahdoo.other
{
    public class CameraFacingBillboard : MonoBehaviour
    {


        private Camera m_Camera;

        void Awake()
        {
            m_Camera = Camera.main;

            transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
                m_Camera.transform.rotation * Vector3.up);
        }

        void Update()
        {
            transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
                m_Camera.transform.rotation * Vector3.up);
        }


    }
}


