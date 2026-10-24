
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject achievementsPanel;
    public GameObject optionsPanel;

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenAchievements()
    {
        achievementsPanel.SetActive(true);
    }

    public void CloseAchievements()
    {
        achievementsPanel.SetActive(false);
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Zamykanie gry...");
        Application.Quit();
    }
}
