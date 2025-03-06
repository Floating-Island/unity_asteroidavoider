using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] PlayerMovement playerObject;
    [SerializeField] AsteroidSpawner asteroidSpawner;

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] ScoreSystem scoreSystem;
    [SerializeField] TMP_Text scoreText;

    [SerializeField] private Button continueButton;

    public void EndGame()
    {
        playerObject.gameObject.SetActive(false);

        asteroidSpawner.Pause();

        gameOverPanel.SetActive(true);

        scoreSystem.enabled = false;
        scoreText.text = "Score: " + ScoreSystem.HighScore();

        continueButton.interactable = true;
    }
    public void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public void Replay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void Continue()
    {
        continueButton.interactable = false;
        AdManager.Instance.ShowAd(this);
    }

    public void ContinueAfterAd()
    {
        playerObject.ResetState();
        asteroidSpawner.Resume();
        scoreSystem.enabled = true;
        //gameOverPanel.SetActive(false);
    }
}
