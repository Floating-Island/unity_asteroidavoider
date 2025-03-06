using TMPro;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] PlayerMovement playerObject;
    [SerializeField] AsteroidSpawner asteroidSpawner;

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] ScoreSystem scoreSystem;
    [SerializeField] TMP_Text scoreText;

    public void EndGame()
    {
        playerObject.gameObject.SetActive(false);

        asteroidSpawner.StopSpawningAsteroids();
        asteroidSpawner.enabled = false;

        gameOverPanel.SetActive(true);

        scoreSystem.enabled = false;
        scoreText.text = "Score: " + ScoreSystem.HighScore();
    }
    public void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public void Replay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
