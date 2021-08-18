using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessInput : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float ThrustAmount = 10000;
    [SerializeField] float RotationAmount = 200;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessRotation();
        ProcessThrust();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.D)){
            Debug.Log("Pressed D");
            ApplyRotation(true);
        } else if (Input.GetKey(KeyCode.A)){
            Debug.Log("Pressed A");
            ApplyRotation(false);
        }
    }

    void ProcessThrust()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("pressed up");
            rb.AddRelativeForce(Vector3.up * ThrustAmount * Time.deltaTime);
        }
    }

    void ApplyRotation(bool forward)
    {
        // freeze rotation to manually rotate
        rb.freezeRotation = true;
        float RotationAmt = RotationAmount;
        if (!forward) {
            RotationAmt = - RotationAmt;
        }
        transform.Rotate(Vector3.forward * RotationAmt * Time.deltaTime);
        rb.freezeRotation = false;

    }
}
