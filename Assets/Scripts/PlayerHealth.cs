using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameOverHandler gameOverHandler;
    public void Crash()
    {
        gameOverHandler.EndGame();
    }
}
