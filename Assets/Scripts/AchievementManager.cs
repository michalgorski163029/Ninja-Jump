
using System.Collections;
using UnityEngine;
using TMPro;

public class AchievementManager : MonoBehaviour
{
    public GameObject achievementPopup;
    public TextMeshProUGUI achievementText;

    private Coroutine popupCoroutine;

    public void CheckAchievements(int score)
    {
        if (score >= 50)
        {
            UnlockAchievement(
                "Achievement50",
                "Legenda Ninja"
            );
        }
        else if (score >= 25)
        {
            UnlockAchievement(
                "Achievement25",
                "Mistrz odbić"
            );
        }
        else if (score >= 10)
        {
            UnlockAchievement(
                "Achievement10",
                "Pierwsze kroki"
            );
        }
    }

    void UnlockAchievement(string key, string name)
    {
        if (PlayerPrefs.GetInt(key, 0) == 1)
            return;

        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();

        Debug.Log("Odblokowano: " + name);

        if (popupCoroutine != null)
            StopCoroutine(popupCoroutine);

        popupCoroutine = StartCoroutine(
            ShowPopup(name)
        );
    }

    IEnumerator ShowPopup(string name)
    {
        achievementText.text =
            "OSIĄGNIĘCIE!\n" + name;

        achievementPopup.SetActive(true);

        yield return new WaitForSecondsRealtime(3f);

        achievementPopup.SetActive(false);
        popupCoroutine = null;
    }
}
