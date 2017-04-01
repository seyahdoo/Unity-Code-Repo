using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace seyahdoo.controlls
{

    public class MouseLookSimple : MonoBehaviour
    {

        public float Sensivity = 2f;
        public bool working = false;

        void Update()
        {

            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
                working = true;
            }

            if (!working) return;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                working = false;
            }
           
            transform.localEulerAngles +=
                new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0)
                * Sensivity;
        }

    }

}
