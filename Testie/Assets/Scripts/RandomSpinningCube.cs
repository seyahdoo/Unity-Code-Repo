using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpinningCube : MonoBehaviour {

    //This class will be awesome!

    public Vector3 rotationSpeed;
    public float maxRotationSpeed = 1;

    void Awake()
    {
        rotationSpeed = new Vector3(
            Random.Range(-maxRotationSpeed, +maxRotationSpeed),
            Random.Range(-maxRotationSpeed, +maxRotationSpeed),
            Random.Range(-maxRotationSpeed, +maxRotationSpeed));
    }

    void FixedUpdate()
    {
        transform.localEulerAngles += rotationSpeed;

    }

}
