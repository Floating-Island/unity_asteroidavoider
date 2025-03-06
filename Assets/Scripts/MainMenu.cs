using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text highScoreText;

    private void Start()
    {
        highScoreText.text = "High Score: " + ScoreSystem.HighScore();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
