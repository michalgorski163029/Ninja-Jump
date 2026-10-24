
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI bestScoreText;

    private bool isGameOver = false;

    void Start()
    {
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
    }

    public void GameOver(int score)
    {
        if (isGameOver)
            return;

        isGameOver = true;

        // Zatrzymujemy muzykę po śmierci
        GameObject musicObject =
            GameObject.Find("GameMusic");

        if (musicObject != null)
        {
            AudioSource music =
                musicObject.GetComponent<AudioSource>();

            if (music != null)
            {
                music.Stop();
            }
        }

        // Zapisujemy rekord
        int best = PlayerPrefs.GetInt("BestScore", 0);

        if (score > best)
        {
            best = score;
            PlayerPrefs.SetInt("BestScore", best);
            PlayerPrefs.Save();
        }

        bestScoreText.text = "Best: " + best;

        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
