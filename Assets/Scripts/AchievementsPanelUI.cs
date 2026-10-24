
using UnityEngine;
using TMPro;

public class AchievementsPanelUI : MonoBehaviour
{
    public TextMeshProUGUI achievement10Text;
    public TextMeshProUGUI achievement25Text;
    public TextMeshProUGUI achievement50Text;

    private void OnEnable()
    {
        RefreshAchievements();
    }

    public void RefreshAchievements()
    {
        UpdateAchievement(
            achievement10Text,
            "Achievement10",
            "Pierwsze kroki",
            10
        );

        UpdateAchievement(
            achievement25Text,
            "Achievement25",
            "Mistrz odbić",
            25
        );

        UpdateAchievement(
            achievement50Text,
            "Achievement50",
            "Legenda Ninja",
            50
        );
    }

    private void UpdateAchievement(
        TextMeshProUGUI text,
        string key,
        string achievementName,
        int requiredScore)
    {
        if (text == null)
            return;

        bool unlocked =
            PlayerPrefs.GetInt(key, 0) == 1;

        if (unlocked)
        {
            text.text = "✓ " + achievementName +
                        "\nODBLOKOWANE";

            text.color = Color.green;
        }
        else
        {
            text.text = achievementName +
                        "\nZdobądź " +
                        requiredScore +
                        " punktów - ZABLOKOWANE";

            text.color = Color.white;
        }
    }
}
