using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowingScore : MonoBehaviour
{
    private float _chewingScore;

    


    // Start is called before the first frame update
    void Start()
    {
        _chewingScore = GameManager.GetScore();
    }

    // Update is called once per frame
    void Update()
    {
        BlowingTimer();
    }

    private void BlowingTimer()
    {
        
        if (_chewingScore > 0f)
        {
            _chewingScore -= Time.deltaTime;
            print(_chewingScore);

        }

    }

}
