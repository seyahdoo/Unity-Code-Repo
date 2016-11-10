///Author: seyahdoo(Seyyid Ahmed Doğan) 
///github: github.com/seyahdoo
///email : contact@seyahdoo.com
///
///see my other reusable unity codes at
///https://github.com/seyahdoo/Unity-Code-Repo

using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {

    public Transform TargetTransform;
    public Transform MyTransform;
    public float SpeedPosition = 0.1f;
    public float SpeedRotation = 0.1f;

    public bool Following = true;

    void Update()
    {
        if(Following)
        {
            //Go to place According to Speed
            MyTransform.position = Vector3.Lerp(MyTransform.position, TargetTransform.position, SpeedPosition);
            MyTransform.rotation = Quaternion.Slerp(MyTransform.rotation, TargetTransform.rotation, SpeedRotation);
        }
        
    }
}

