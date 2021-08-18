using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentSceneIndex;
    AudioSource audioSource;
    bool isTransitioning;

    [SerializeField] float LoadDelay = 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;



    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
        isTransitioning = false;
    }
    private void OnCollisionEnter(Collision other) {
        if(!isTransitioning)
        {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("bumped into friendly.");
                    break;
                case "Finish":
                    StartLandingSequence();
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }
    }

    void FreezeInput()
    {
        GetComponent<ProcessInput>().enabled = false;
    }
    void StartCrashSequence()
    {
        // TODO: add partical effect
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        FreezeInput();
        InvokeParticles(crashParticles);
        Invoke("ReloadLevel", LoadDelay);
    }

    void InvokeParticles(ParticleSystem particles)
    {
        particles.Play();
    }

    void StartLandingSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        FreezeInput();
        InvokeParticles(successParticles);
        Invoke("LoadNextLevel", LoadDelay);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex > SceneManager.sceneCount)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
