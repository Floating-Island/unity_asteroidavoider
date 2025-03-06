using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] PlayerMovement playerObject;
    [SerializeField] AsteroidSpawner asteroidSpawner;

    [SerializeField] GameObject gameOverPanel;

    public void EndGame()
    {
        playerObject.gameObject.SetActive(false);
        asteroidSpawner.StopSpawningAsteroids();
        asteroidSpawner.enabled = false;
        gameOverPanel.SetActive(true);
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
