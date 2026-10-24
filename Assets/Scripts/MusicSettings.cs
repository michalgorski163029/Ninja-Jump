
using UnityEngine;
using UnityEngine.UI;

public class MusicSettings : MonoBehaviour
{
    public Slider musicSlider;

    void Start()
    {
        float savedVolume =
            PlayerPrefs.GetFloat("MusicVolume", 0.5f);

        musicSlider.SetValueWithoutNotify(savedVolume);
        AudioListener.volume = savedVolume;

        musicSlider.onValueChanged.AddListener(
            SetMusicVolume
        );
    }

    public void SetMusicVolume(float volume)
    {
        AudioListener.volume = volume;

        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
}
