using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlocks : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float movingSeconds;
    [SerializeField] string direction;

    [SerializeField] string rotation;
    [SerializeField] float rotationSpeed;
    float timeStart;

    void Start()
    {
        speed = 8f;
        movingSeconds = 5f;
        timeStart = Time.time;
        rotationSpeed = 100f;
    }

    void Update()
    {
        ApplyMovement();
        ApplyRotation();
    }

    void ApplyMovement()
    {
        if (Time.time - timeStart >= movingSeconds)
        {
            speed = -speed;
            timeStart = Time.time;
        }
        switch (direction)
        {
            case "right":
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                break;
            case "up":
                transform.Translate(Vector3.up * speed * Time.deltaTime);
                break; 
            case "left":
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                break;
            case "down":
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                break;
            default:
                break;
        }
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
