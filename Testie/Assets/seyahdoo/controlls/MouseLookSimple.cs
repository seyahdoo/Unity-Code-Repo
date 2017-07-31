///Author: seyahdoo(Seyyid Ahmed Doğan) 
///github: github.com/seyahdoo
///email : contact@seyahdoo.com
///
///see my other reusable unity codes at
///https://github.com/seyahdoo/Unity-Code-Repo

///Changelog:
///XX.XX.XXXX -> Created by seyahdoo

///usage:
///Attach to any camera

///Dependancies:
///None

using UnityEngine;

namespace seyahdoo.controlls
{
    [AddComponentMenu("Seyahdoo/Controlls/MouseLookSimple")]
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
