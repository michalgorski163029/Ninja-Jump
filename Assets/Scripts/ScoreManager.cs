
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private int score = 0;
    private AchievementManager achievementManager;

    void Start()
    {
        achievementManager =
            FindFirstObjectByType<AchievementManager>();

        UpdateScore();
    }

    public void AddPoint()
    {
        score++;
        UpdateScore();

        if (achievementManager != null)
        {
            achievementManager.CheckAchievements(score);
        }
    }

    public int GetScore()
    {
        return score;
    }

    void UpdateScore()
    {
        scoreText.text = score.ToString();
    }
}
