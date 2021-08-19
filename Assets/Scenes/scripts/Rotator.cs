using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
   [SerializeField] string rotation;
    [SerializeField] float rotationSpeed;
    float timeStart;

    void Start()
    {

        timeStart = Time.time;
        rotationSpeed = 100f;
    }

    void Update()
    {
        ApplyRotation();
    }

    void ApplyRotation()
    {
        switch (rotation)
        {
            case "clockwise":
                transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
                break;
            case "counter":
                transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
                break;
            default:
                break;
        }
        
    }
}
