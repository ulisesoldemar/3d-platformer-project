using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    //Almacena las diferentes pistas de música
    public AudioSource[] music;
    //Almacena los SFX
    public AudioSource[] sfx;
    public AudioMixerGroup musicMixer, sfxMixer;

    ///Audio para tocar en el nivel
    public int levelMusicToPlay;
    

    private void Awake()
    {
        instance = this;
    }
    //Reproduce música al cargar la escena
    void Start()
    {
        PlayMusic(0);
    }
    public void PlayMusic(int musicToPlay)
    {
        music[musicToPlay].Play();
    }
    //Pausa la reproducción de música
    public void PauseMusic(int musicToPlay)
    {
        music[musicToPlay].Pause();
    }
    //Reproduce SFX
    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Play();
    }
    //Establece el volumen de la música
    public void SetMusicLevel()
    {
        musicMixer.audioMixer.SetFloat("MusicVol", UIManager.Instance.musicVolSlider.value);
    }
    //Establece el volumen de los SFX
    public void SetSFXLevel()
    {
        sfxMixer.audioMixer.SetFloat("SFXVol", UIManager.Instance.sfxVolSlider.value);
    }
}
