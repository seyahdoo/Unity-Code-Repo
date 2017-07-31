///Author: seyahdoo(Seyyid Ahmed Doğan) 
///github: github.com/seyahdoo
///email : contact@seyahdoo.com
///
///see my other reusable unity codes at
///https://github.com/seyahdoo/Unity-Code-Repo

///Changelog:
///XX.XX.XXXX -> Copied from internet
///30.07.2017 -> Edited to emulate Vr Camera Controller

///usage:
///Attach to any camera

///Dependancies:
///None

using UnityEngine;

namespace seyahdoo.controlls
{

    public class VrEmulateController : MonoBehaviour
    {

        Transform cam;

        void Awake()
        {
            cam = Camera.main.transform;
            Input.gyro.enabled = true;
            //oldRot = Input.gyro.attitude;
        }

        //Quaternion oldRot;
        //
        //void Update()
        //{
        //
        //    Quaternion rot = Input.gyro.attitude;
        //    //rot.Rotate(0f, 0f, 180f, Space.Self); // Swap "handedness" of quaternion from gyro.
        //    //rot.Rotate(90f, 180f, 0f, Space.World); // Rotate to make sense as a camera pointing out the back of your device.
        //
        //
        //
        //    Vector3 mov = Input.gyro.attitude.eulerAngles - oldRot.eulerAngles;
        //    cam.eulerAngles += mov;
        //
        //
        //
        //    oldRot = Input.gyro.attitude;
        //}

        private float initialYAngle = 0f;
        private float appliedGyroYAngle = 0f;
        public float calibrationYAngle = 0f;

        void Start()
        {
            initialYAngle = cam.eulerAngles.y;
        }

        void Update()
        {
            ApplyGyroRotation();
            ApplyCalibration();
        }


        public void CalibrateYAngle()
        {
            calibrationYAngle = appliedGyroYAngle - initialYAngle; // Offsets the y angle in case it wasn't 0 at edit time.
        }

        void ApplyGyroRotation()
        {
            cam.rotation = Input.gyro.attitude;
            cam.Rotate(0f, 0f, 180f, Space.Self); // Swap "handedness" of quaternion from gyro.
            cam.Rotate(90f, 180f, 0f, Space.World); // Rotate to make sense as a camera pointing out the back of your device.
            appliedGyroYAngle = cam.eulerAngles.y; // Save the angle around y axis for use in calibration.
        }

        void ApplyCalibration()
        {
            cam.Rotate(0f, -calibrationYAngle, 0f, Space.World); // Rotates y angle back however much it deviated when calibrationYAngle was saved.
        }

        
    }

}
