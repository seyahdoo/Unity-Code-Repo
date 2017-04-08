using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace seyahdoo.controlls
{

    [AddComponentMenu("Seyahdoo/Controlls/FirstPersonFloatingCamera")]
    public class FirstPersonFloatingCamera : MonoBehaviour
    {
        public bool Working = false;

        [Range(0, 5)]
        public float Sensivity = 2f;
        [Range(0, 2)]
        public float MoveSpeed = .2f;
        [Range(0, 2)]
        public float ShiftSpeedBoost = 1f;

        private float _moveSpeed = .2f;
        private float _ySpeed = 0;


        void Update()
        {
            //LClick to lock cursor
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Working = true;
            }
            
            //If im now workin, there's nothing to do
            if (!Working) return;

            //ESC to unlock cursor 
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                Working = false;
            }

            //LSHIFT to boost
            if (Input.GetKey(KeyCode.LeftShift))
                _moveSpeed = ShiftSpeedBoost;
            else
                _moveSpeed = MoveSpeed;

            //Handle QE
            if (Input.GetKey(KeyCode.Q)) _ySpeed = Mathf.Lerp(_ySpeed, -1, .05f);
            else if (Input.GetKey(KeyCode.E)) _ySpeed = Mathf.Lerp(_ySpeed, 1, .05f);
            else _ySpeed = Mathf.Lerp(_ySpeed, 0, .1f);

            //turn camera
            transform.localEulerAngles +=
                new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0)
                * Sensivity;
            
            //move camera
            transform.Translate(
                new Vector3(Input.GetAxis("Horizontal"),_ySpeed, Input.GetAxis("Vertical"))
                * _moveSpeed);
            
        }

    }

}
