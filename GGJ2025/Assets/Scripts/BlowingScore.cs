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
        // _pressBtn = true;
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
            _timerSlider.value = _chewingScore;
            // print(_chewingScore);
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                _currentBtnTimer = _btnPressTimer;
                _pressBtn = true;    
                
            }

            if (_pressBtn)
            {
                // timer between each press
                // when timer reaches 0 _pressBtn = false
                // then need to press button again.
                
                BtnTimer();
                
            }
            

            if (Input.GetKey(KeyCode.Space) && _pressBtn)
            {
                Debug.Log("Blowing Bubble Gum");
                IncreaseScale();

            }
           

            

            // show button to press 
            // if(btn pressed)
            //  can still press space
            // else
            // cant press space

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
        Vector3 scale;
        scale.x = _testSprite.transform.localScale.x + Time.deltaTime * blowRate;
        scale.y = _testSprite.transform.localScale.y + Time.deltaTime * blowRate;
        scale.z = 0;
        _testSprite.transform.localScale = scale;

    }

    private void BtnTimer()
    {
        
        // _pressBtn = true;
        if (_currentBtnTimer > 0)
        {
            _currentBtnTimer -= Time.deltaTime;
            print(_currentBtnTimer);
            // _pressBtn = true;

        }
        if (_currentBtnTimer <= 0 )
        {
            _pressBtn = false;
            Debug.Log("Press button");
        }
        

    }

}
