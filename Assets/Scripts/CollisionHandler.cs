using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) 
    {
        switch(other.gameObject.tag)
        {
            case "Fuel":
                Debug.Log("Fuel!");
                break;
            case "Finish":
                Debug.Log("Finished!");
                break;
            case "Friendly":
                Debug.Log("Friendly!");
                break;
            default:
                Debug.Log("Nothing to report");
                ReloadScene();
                break;
        }
    }

    void ReloadScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
