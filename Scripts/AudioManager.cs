using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    [SerializeField] AudioMixer mixer; 
    [SerializeField] AudioSource bgm, victory;
    [SerializeField] AudioSource[] soundEffects;
    

    public const string MUSIC_KEY = "MusicVolume";
    public const string SFX_KEY = "SFXVolume";

    void Awake()
    {
        if (instance == null) 
        {
            instance = this;            
        }
        else { Destroy(gameObject); }

        LoadVolume();
    }




    void LoadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1);

        mixer.SetFloat(VolumeSetting.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(VolumeSetting.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
    }







    public void StopBGM()
    {
        bgm.Stop();
    }


    public void PlayLevelVictory()
    {
        StopBGM();
        victory.Play();
    }


    public void PlaySFX(int sfxNumber)
    {
        soundEffects[sfxNumber].Stop();
        soundEffects[sfxNumber].Play();
    }


    public void StopSFX(int sfxNumber)
    {
        soundEffects[sfxNumber].Stop();
    }

}
