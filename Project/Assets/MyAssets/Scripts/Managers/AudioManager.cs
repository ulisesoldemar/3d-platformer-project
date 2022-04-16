using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    //Almacena las diferentes pistas de m�sica
    public AudioSource[] music;
    //Almacena los SFX
    public AudioSource[] sfx;
    public AudioMixerGroup musicMixer, sfxMixer;
    

    private void Awake()
    {
        instance = this;
    }
    //Reproduce m�sica al cargar la escena
    void Start()
    {
        PlayMusic(0);
    }
    public void PlayMusic(int musicToPlay)
    {
        music[musicToPlay].Play();
    }
    //Pausa la reproducci�n de m�sica
    public void PauseMusic(int musicToPlay)
    {
        music[musicToPlay].Pause();
    }
    //Reproduce SFX
    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Play();
    }
    //Establece el volumen de la m�sica
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
