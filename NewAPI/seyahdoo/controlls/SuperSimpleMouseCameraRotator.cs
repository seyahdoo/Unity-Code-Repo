using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace seyahdoo.controlls
{

    public class SuperSimpleMouseCameraRotator : MonoBehaviour
    {

        void Update()
        {

            transform.localEulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);

        }

    }

}
