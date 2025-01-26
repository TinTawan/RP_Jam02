using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sounds_Game[] GameMusic,GameSfx; 
    public AudioSource GameMusicSource, GameSfxSource;
   

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { 
        
        Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayMusic("Music1");
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float savedSfxVolume = PlayerPrefs.GetFloat("SfxVolume", 1f);

        VolumeGameMusic(savedMusicVolume);
        VolumeGameSfx(savedSfxVolume);
    }

    public void PlayMusic(string name) 
    {
        Sounds_Game Sound = Array.Find(GameMusic, x => x.Name == name);
        if (Sound == null)
        {

            Debug.Log("No music found");

        }
        else {
            GameMusicSource.clip = Sound.audioClip;
            GameMusicSource.Play();
        
        }
       

    }
    public void StopMusic()
    {
        GameMusicSource.Pause();
    }
    public void PlaySfx(string name)
    {
        Sounds_Game Sound = Array.Find(GameSfx, x => x.Name == name);
        if (Sound == null)
        {

            Debug.Log("No music found");

        }
        else
        {
            GameSfxSource.PlayOneShot(Sound.audioClip);
          

        }

    }

   
    public void VolumeGameMusic(float volume)
    {
        GameMusicSource.volume = volume;

        PlayerPrefs.SetFloat("MusicVolume", volume);
    
    }
    public void VolumeGameSfx(float volume)
    {
   
        GameSfxSource.volume = volume;
       
        PlayerPrefs.SetFloat("SfxVolume", volume);
    }



}
