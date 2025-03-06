using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier = 1f;
    public const string highScoreKey = "Key_HighScore";

    private float score;

    // Update is called once per frame
    void Update()
    {
        IncreaseScore();
    }

    private void IncreaseScore()
    {
        score += scoreMultiplier * Time.deltaTime;
        scoreText.text = "Score: " + GetScore();
    }

    private void OnDisable()
    {
        SaveScore();
        scoreText.enabled = false;
    }

    private int GetScore()
    {
        return Mathf.FloorToInt(score);
    }

    private void OnDestroy()
    {
        SaveScore();
    }

    public void SaveScore()
    {
        int currentHighScore = PlayerPrefs.GetInt(highScoreKey, 0);
        int currentScore = GetScore();
        if (currentScore > currentHighScore)
        {
            PlayerPrefs.SetInt(highScoreKey, currentScore);
        }
    }

    public static int HighScore()
    {
        return PlayerPrefs.GetInt(highScoreKey, 0);
    }
}
