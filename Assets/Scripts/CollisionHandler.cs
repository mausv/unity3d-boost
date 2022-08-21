using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayBeforeAction = 1.0f;
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
                Debug.Log("Nothing to report");
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        // TODO: Add SFX upon crash
        // TODO: Add particle effect upon crash
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
        // TODO: Add SFX upon success
        // TODO: Add particle effect upon success
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
