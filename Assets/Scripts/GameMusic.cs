
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.spatialBlend = 0f;

        // Głośność zapisana w OPTIONS
        AudioListener.volume =
            PlayerPrefs.GetFloat("MusicVolume", 0.5f);
    }
}
