using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class BlowingScore : MonoBehaviour
{
    private float _chewingScore;
    [SerializeField] private GameObject _testSprite;
    [SerializeField] private float blowRate;
    [SerializeField] private Slider _timerSlider;
    private bool _btnPressed = false;
    [SerializeField] private float _btnPressTimer;
    [SerializeField] private GameObject _dangerSign;
    [SerializeField] private float _signTimer;

    private Image _dangerSign_img;

    private float _currentBtnTimer;
    [SerializeField] private float _burstDuration;
    private bool _isHoldingSpace = false;
    private float _burstTimer;

    
    private void Awake()
    {
        _chewingScore = GameManager.GetChewScore();
        _timerSlider.maxValue = _chewingScore;
        _timerSlider.value = _timerSlider.maxValue;

        _dangerSign_img = _dangerSign.GetComponent<Image>();
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
            CheckForSpacePress();
            
            _chewingScore -= Time.deltaTime;
            _timerSlider.value = _chewingScore;
            
            // Starts timer for next press 
            if (Input.GetKeyDown(KeyCode.W))
            {
                _currentBtnTimer = _btnPressTimer;
                  
                _btnPressed = true;    
                
            }

            

            // checks for button press to start timer
            if (_btnPressed)
            {   
                BtnTimer();   
            }
            
            // To blow the bubblegum
            if (Input.GetKey(KeyCode.Space) && _btnPressed)
            {
                // Debug.Log("Blowing Bubble Gum");
                IncreaseScale();

            }


        }
        else
        {
            // Debug.Log("Times up!");
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
            _btnPressed = false;
            StartCoroutine(Danger());
            Debug.Log("Press button");
        }
        

    }

    private IEnumerator Danger()
    {
        while(!_btnPressed)
        {
            _dangerSign.SetActive(true);
            yield return new WaitForSeconds(_signTimer + (_signTimer/2));
            _dangerSign.SetActive(false);    
            yield return new WaitForSeconds(_signTimer);
        }
    }

    private void CheckForSpacePress()
    {
        if (!_btnPressed)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _burstTimer += Time.deltaTime;
                Debug.Log("Burst Timer" + _burstTimer);

                if (_burstTimer >= _burstDuration)
                {
                    Debug.Log("LOSER!!!");
                }

            }
            else
            {
                _burstTimer = 0f;
            }
        }
        // _burstTimer = 0f;
    }

}
