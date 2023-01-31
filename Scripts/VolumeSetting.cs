using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] AudioMixer masterAudio;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";


    void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        musicSlider.onValueChanged.AddListener(SetSFXVolume);
    }


    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1);
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1);
    }


    private void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, sfxSlider.value);
    }


    void SetMusicVolume(float value)
    {
        masterAudio.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }


    void SetSFXVolume(float value)
    {
        masterAudio.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }
}
