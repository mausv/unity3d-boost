using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayBeforeAction = 1.0f;
    [SerializeField] AudioClip rocketCrash;
    [SerializeField] AudioClip landingSuccess;

    [SerializeField] ParticleSystem rocketCrashParticles;
    [SerializeField] ParticleSystem landingSuccessParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) return;

        switch (other.gameObject.tag)
        {
            case "Finish":
                Debug.Log("Finished!");
                StartLoadNextLevel();
                break;
            case "Friendly":
                Debug.Log("Friendly!");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        // TODO: Add particle effect upon crash
        audioSource.PlayOneShot(rocketCrash);
        rocketCrashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayBeforeAction);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void StartLoadNextLevel()
    {
        isTransitioning = true;
        audioSource.Stop();
        // TODO: Add particle effect upon success
        audioSource.PlayOneShot(landingSuccess);
        landingSuccessParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayBeforeAction);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIdx = currentSceneIndex + 1;
        if (nextSceneIdx == SceneManager.sceneCountInBuildSettings) nextSceneIdx = 0;
        SceneManager.LoadScene(nextSceneIdx);
    }
}
