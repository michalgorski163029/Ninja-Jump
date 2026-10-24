
using UnityEngine;

public class BoundaryDeath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Reagujemy tylko na ninja
        if (!other.CompareTag("Player"))
            return;

        // Znajdujemy GameOverManager
        GameOverManager gameOver =
            FindFirstObjectByType<GameOverManager>();

        // Znajdujemy ScoreManager
        ScoreManager scoreManager =
            FindFirstObjectByType<ScoreManager>();

        if (gameOver == null || scoreManager == null)
        {
            Debug.LogError(
                "Nie znaleziono GameOverManager lub ScoreManager!"
            );
            return;
        }

        // Pobieramy aktualny wynik
        int currentScore = scoreManager.GetScore();

        // Uruchamiamy Game Over
        gameOver.GameOver(currentScore);
    }
}
