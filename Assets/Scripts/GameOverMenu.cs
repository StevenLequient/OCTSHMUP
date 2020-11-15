using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenu;            // Reference to the pause menu canvas as a whole
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        gameOverMenu.SetActive(false);     // Disable and hides the pause menu at the start of the game
    }

    // Update is called once per frame
    void Update()
    {
        if (TetrisController.Instance.dead)
        {
            scoreText.text = "" + TetrisController.Instance.score;
            gameOverMenu.SetActive(true);
        }
    }

    // Restarts the current scene
    public void RestartGame()
    {
        gameOverMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
