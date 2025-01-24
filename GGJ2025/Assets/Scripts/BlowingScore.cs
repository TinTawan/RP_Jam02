using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowingScore : MonoBehaviour
{
    private float _chewingScore = 12;



    // Start is called before the first frame update
    void Start()
    {
        
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
