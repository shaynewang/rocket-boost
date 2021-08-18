using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentSceneIndex;
    AudioSource audioSource;
    bool isTransitioning;
    bool disableCollision;

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
        disableCollision = false;
        GetComponent<Renderer>().enabled = true;
    }

    void Update() {
        ProcessDebug();
    }
    private void OnCollisionEnter(Collision other) {
        if(!isTransitioning && !disableCollision)
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

    void HideRocket()
    {
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach(Renderer r in rs) {
            if (r.tag == "RocketBody")
            {
              r.enabled = false;
            }
        }
    }
    void InvokeParticles(ParticleSystem particles, bool hide_rocket=false)
    {
        if (hide_rocket)
        {
            HideRocket();
        }
        particles.Play();
    }

    void StartCrashSequence()
    {
        // TODO: add partical effect
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        FreezeInput();
        InvokeParticles(crashParticles, hide_rocket: true);
        Invoke("ReloadLevel", LoadDelay);
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

    void ProcessDebug()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        } else if (Input.GetKeyDown(KeyCode.C))
        {
            // Toggle disable collision
            disableCollision = !disableCollision;
        }
    }
}
