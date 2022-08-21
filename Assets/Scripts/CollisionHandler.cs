using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayBeforeAction = 1.0f;
    [SerializeField] AudioClip rocketCrash;
    [SerializeField] AudioClip landingSuccess;

    AudioSource audioSource;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
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
        // TODO: Add particle effect upon crash
        audioSource.PlayOneShot(rocketCrash);
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
        // TODO: Add particle effect upon success
        audioSource.PlayOneShot(landingSuccess);
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
