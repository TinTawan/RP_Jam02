using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class BlowingScore : MonoBehaviour
{
    private float _chewingScore;
    [SerializeField] private GameObject _testSprite;
    [SerializeField] private float blowRate;
    [SerializeField] private Slider _timerSlider;
    private bool _pressBtn = false;
    [SerializeField] private float _btnPressTimer;

    private float _currentBtnTimer;

    
    private void Awake()
    {
        _chewingScore = GameManager.GetChewScore();
        _timerSlider.maxValue = _chewingScore;
        _timerSlider.value = _timerSlider.maxValue;
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
        // takes the score from the chewing phase to use as a timer.
        if (_chewingScore > 0f)
        {
            _chewingScore -= Time.deltaTime;
            _timerSlider.value = _chewingScore;
            
            // Starts timer for next press 
            if (Input.GetKeyDown(KeyCode.W))
            {
                _currentBtnTimer = _btnPressTimer;
                _pressBtn = true;    
                
            }

            // checks for button press to start timer
            if (_pressBtn)
            {   
                BtnTimer();   
            }
            
            // To blow the bubblegum
            if (Input.GetKey(KeyCode.Space) && _pressBtn)
            {
                Debug.Log("Blowing Bubble Gum");
                IncreaseScale();

            }


        }
        else
        {
            Debug.Log("Times up!");
        }

    }

    private void IncreaseScale()
    {
        Vector3 scale;
        scale.x = _testSprite.transform.localScale.x + Time.deltaTime * blowRate;
        scale.y = _testSprite.transform.localScale.y + Time.deltaTime * blowRate;
        scale.z = 0;
        _testSprite.transform.localScale = scale;

    }

    private void BtnTimer()
    {   
        if (_currentBtnTimer > 0)
        {
            _currentBtnTimer -= Time.deltaTime;
            print(_currentBtnTimer);

        }
        if (_currentBtnTimer <= 0 )
        {
            _pressBtn = false;
            Debug.Log("Press button");
        }
        

    }

}
