using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillation : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0, 1)] float movementFactor;
    [SerializeField] float period = 2f;
    const float tau = Mathf.PI * 2;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Oscillate();
    }

    void Oscillate()
    {
        if (!movementVector.Equals(Vector3.zero))
        {
            float cycles = Time.time / period;
            float sine = Mathf.Sin(cycles * tau);
            movementFactor = (sine + 1f) / 2f;
            Vector3 offset = movementVector * movementFactor;
            transform.position = startingPosition + offset;
        } else {
            Debug.Log("===============");
            Debug.Log(gameObject.name);
        }

    }
}
