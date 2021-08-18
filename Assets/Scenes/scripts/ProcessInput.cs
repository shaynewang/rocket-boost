using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessInput : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float ThrustAmount = 10000;
    [SerializeField] float RotationAmount = 200;
    [SerializeField] AudioClip thrustSound;
    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
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
            ApplyRotation(true);
            if(!leftBoosterParticles.isPlaying)
            {
                leftBoosterParticles.Play();
            }
        } else if (Input.GetKey(KeyCode.A)){
            ApplyRotation(false);
            if(!rightBoosterParticles.isPlaying)
            {
                rightBoosterParticles.Play();
            }
        } else {
            rightBoosterParticles.Stop();
            leftBoosterParticles.Stop();
        }
    }

    void ProcessThrust()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * ThrustAmount * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.Play();
            mainBoosterParticles.Play();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            audioSource.Stop();
            mainBoosterParticles.Stop();
        }
    }

    void ApplyRotation(bool forward)
    {
        // freeze rotation to manually rotate
        // forward is right
        rb.freezeRotation = true;
        float RotationAmt = RotationAmount;
        // rotation to left
        if (!forward) {
            RotationAmt = - RotationAmt;
        }
        transform.Rotate(Vector3.forward * RotationAmt * Time.deltaTime);
        rb.freezeRotation = false;

    }
}
