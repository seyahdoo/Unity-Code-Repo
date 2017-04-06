using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curvess : MonoBehaviour {

    //This class will be awesome!


    public AnimationCurve curve;

    void Update()
    {
        transform.localScale = new Vector3(curve.Evaluate(Time.time), curve.Evaluate(Time.time), curve.Evaluate(Time.time));
    }


}
