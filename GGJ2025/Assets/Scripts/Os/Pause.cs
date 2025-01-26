using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
  public static bool paused = false;
    public GameObject MenuUI;
    private AudioManager audioManager;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (paused)
            {
                Resume();
                
            }
            else {
                
                Stop();
            }
           /* AudioManager.instance.PlaySfx("ClickE");
            AudioManager.instance.PlayMusic("Blow");*/
        }
    }
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    public void Resume()
    {
        
        MenuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
        if (audioManager != null)
        {
            audioManager.GameMusicSource.UnPause();
        }

    }
    void Stop()
    {
        MenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
        if (audioManager != null)
        {
            audioManager.GameMusicSource.Pause();
        }
    }

    

}
