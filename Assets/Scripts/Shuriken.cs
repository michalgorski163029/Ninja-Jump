
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Ninja")
        {
            ScoreManager scoreManager =
                FindFirstObjectByType<ScoreManager>();

            GameOverManager gameOverManager =
                FindFirstObjectByType<GameOverManager>();

            gameOverManager.GameOver(
                scoreManager.GetScore()
            );
        }
    }
}
