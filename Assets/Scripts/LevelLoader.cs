using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;           // The current animation length is 1 second

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));        // The function is an asynchronous coroutine, so it's wrapped in StartCoroutine       
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        
        transition.SetTrigger("Start");                         // Sets off the trigger of the animator which plays the animation
        yield return new WaitForSeconds(transitionTime);        // Wait for the animation to complete
        SceneManager.LoadScene(levelIndex);                     // Loads the next level scene from the scene build settings
    }

}
