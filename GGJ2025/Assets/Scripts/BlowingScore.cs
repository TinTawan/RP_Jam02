using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class BlowingScore : MonoBehaviour
{
    private float _chewingScore;
    [SerializeField] private GameObject _testSprite;

    
    private void Awake()
    {
        _chewingScore = GameManager.GetScore();
    }

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

            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("Blowing Bubble Gum");
                IncreaseScale();
            }
            // If scale is too big move camera backwards 
            // while pressing space
            // increase scale by delta time
            // when button press comes up
            // space can sitll be pressed 
            // timer for button press (short)
            // if not pressed 
            // bubble pops
            // else 
            // can keeping blowing bubble


        }
        else
        {
            Debug.Log("Times up!");
        }

    }

    private void IncreaseScale()
    {
        //_testSprite.transform.localScale = Time.deltaTime * 2;
        Vector3 scale;
        scale.x = _testSprite.transform.localScale.x + Time.deltaTime * 2;
        scale.y = _testSprite.transform.localScale.y + Time.deltaTime * 2;
        scale.z = 0;
        _testSprite.transform.localScale = scale;

    }

}
