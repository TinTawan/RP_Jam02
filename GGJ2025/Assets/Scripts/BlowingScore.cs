using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlowingScore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _testSprite;
    [SerializeField] private Slider _timerSlider;
    [SerializeField] private GameObject _dangerSign;

    [Header("Values")]
    [SerializeField] private float blowRate;
    [SerializeField] private float _btnPressTimer;
    [SerializeField] private float _signTimer;
    [SerializeField] private float _burstDuration;  
    [SerializeField] private Color _startColour;
    [SerializeField] private Color _endColour;

    private float _chewingScore;
    private bool _btnPressed = false;
    private Image _dangerSign_img;

    private float _currentBtnTimer;
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
                _dangerSign_img.color = Color.Lerp(_startColour, _endColour, _burstTimer);
                Debug.Log("Burst Timer" + _burstTimer);

                if (_burstTimer >= _burstDuration)
                {
                    Debug.Log("LOSER!!!");
                }

            }
            else
            {   
                _dangerSign_img.color = Color.Lerp(_endColour, _startColour, _burstDuration);
                _burstTimer = 0f;
            }
        }
        // _burstTimer = 0f;
    }

}
