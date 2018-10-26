using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using seyahdoo.pooling.v3;

public class RandomSpinningCube : MonoBehaviour, IPoolable {

    //This class will be awesome!

    public Vector3 rotationSpeed;
    public float maxRotationSpeed = 1;

    public void OnPoolGet()
    {
        //Debug.Log("Get Object from pool");
        Awake();
        this.gameObject.SetActive(true);
    }

    public void OnPoolInstantiate()
    {
        gameObject.SetActive(false);
    }

    public void OnPoolRelease()
    {
        //Debug.Log("Release object to pool");
        gameObject.SetActive(false);
    }

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
