using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    private static float _chewScore = 20;
    private static float _volume;

    private static float _blowScore;
    private static bool hasWon;


    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(this);
        }
        else if (manager!=this) 
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static float GetChewScore()
    {
        return _chewScore;
    }

    public static float GetBlowScore()
    {
        return _blowScore;
    }

    public static float GetVol()
    {
        return _volume;
    }

    public static void SetVol(float vol)
    {
        _volume = vol;
    }

    public static void SetChewScore(float score)
    {
        _chewScore = 5 + score;
    }

    public static void SetBlowScore(float score)
    {
        _blowScore = score;
    }

    public static void SetHasWon(bool won)
    {
        hasWon = won;
    }

    public static bool GetHasWon()
    {
        return hasWon;
    }
}
