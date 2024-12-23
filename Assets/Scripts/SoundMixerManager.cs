using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider soundFXVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundFoleysVolumeSlider;

    [SerializeField] private float defaultMasterVolume = 0.8f;
    [SerializeField] private float defaultSoundFXVolume = 1f;
    [SerializeField] private float defaultMusicVolume = 0.5f;
    [SerializeField] private float defaultSoundFoleysVolume = 0.7f;

    private void Awake()
    {
        if (audioMixer == null)
        {
            Debug.LogError("AudioMixer is not assigned!");
            return;
        }

        SetMasterVolume(defaultMasterVolume);
        SetSoundFXVolume(defaultSoundFXVolume);
        SetMusicVolume(defaultMusicVolume);
        SetSoundFoleysVolume(defaultSoundFoleysVolume);

        if (masterVolumeSlider != null)
            masterVolumeSlider.value = defaultMasterVolume;
        if (soundFXVolumeSlider != null)
            soundFXVolumeSlider.value = defaultSoundFXVolume;
        if (musicVolumeSlider != null)
            musicVolumeSlider.value = defaultMusicVolume;
        if (soundFoleysVolumeSlider != null)
            soundFoleysVolumeSlider.value = defaultSoundFoleysVolume;
    }

    public void SetMasterVolume(float level)
    {
        float dB = Mathf.Log10(level > 0.0001f ? level : 0.0001f) * 20f;
        audioMixer.SetFloat("MasterVolume", dB);
        Debug.Log($"MasterVolume set to {level} (dB: {dB})");
    }

    public void SetSoundFXVolume(float level)
    {
        float dB = Mathf.Log10(level > 0.0001f ? level : 0.0001f) * 20f;
        audioMixer.SetFloat("SoundFXVolume", dB);
        Debug.Log($"SoundFXVolume set to {level} (dB: {dB})");
    }

    public void SetMusicVolume(float level)
    {
        float dB = Mathf.Log10(level > 0.0001f ? level : 0.0001f) * 20f;
        audioMixer.SetFloat("MusicVolume", dB);
        Debug.Log($"MusicVolume set to {level} (dB: {dB})");
    }

    public void SetSoundFoleysVolume(float level)
    {
        float dB = Mathf.Log10(level > 0.0001f ? level : 0.0001f) * 20f;
        audioMixer.SetFloat("FoleysVolume", dB);
        Debug.Log($"SoundFoleysVolume set to {level} (dB: {dB})");
    }
}
