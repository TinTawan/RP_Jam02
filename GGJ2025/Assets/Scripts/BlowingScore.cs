using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlowingScore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _bubble;
    [SerializeField] private GameObject _dangerSign;

    [Header("UI References")]
    [SerializeField] private Slider _timerSlider;
    [SerializeField] private TextMeshProUGUI _scoreText, countdownText;
    [SerializeField] private TextMeshProUGUI _inputText;

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

    //Random key input section
    KeyCode randKey;

    //countdown start game
    bool gameStart = false;

    
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
        StartCoroutine(StartGame());

    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            BlowingTimer();
            _scoreText.text = $"Score: {(int)System.Math.Round(_bubble.transform.localScale.x * 100, 2)}";
        }
        
    }

    IEnumerator StartGame()
    {
        countdownText.text = "3";
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        countdownText.text = "";

        gameStart = true;

        randKey = RandomKey();
        Debug.Log(randKey.ToString());

        _btnPressed = true;
    }

    private void BlowingTimer()
    {
        // takes the score from the chewing phase to use as a timer.
        if (_chewingScore > 0f)
        {
            CheckForSpacePress();
            
            _chewingScore -= Time.deltaTime;
            _timerSlider.value = _chewingScore;

            // Starts timer for next press (add random key for input here)
            if (Input.GetKeyDown(randKey))
            {
                _currentBtnTimer = _btnPressTimer;

                _btnPressed = true;

            }
            /*if (Input.GetKeyDown(randKey))
            {
                //correct key pressed
                randKey = RandomKey();

                _currentBtnTimer = _btnPressTimer;

                _btnPressed = true;
                
            }*/



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
            else
            {
                DecreaseScale();
            }


        }
        else
        {
            // Debug.Log("Times up!");
            // Game Over
            // save score
            // "Win condition"
            StopAllCoroutines();
            _dangerSign.SetActive(false);
        }

    }

    private void IncreaseScale()
    {
        Vector3 scale;
        scale.x = _bubble.transform.localScale.x + Time.deltaTime * blowRate;
        scale.y = _bubble.transform.localScale.y + Time.deltaTime * blowRate;
        scale.z = 0;
        _bubble.transform.localScale = scale;

    }
    
    private void DecreaseScale()
    {
        if(_bubble.transform.localScale.x > 0 && _bubble.transform.localScale.y > 0)
        {
            Vector3 scale;
            scale.x = _bubble.transform.localScale.x - Time.deltaTime * blowRate / 3;
            scale.y = _bubble.transform.localScale.y - Time.deltaTime * blowRate / 3;
            scale.z = 0;
            _bubble.transform.localScale = scale;
        }
        

    }
    

    // Timer between each button press 
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

            // Here is where the bubbles spawn in.

            randKey = RandomKey();
            StartCoroutine(Danger());
            Debug.Log("Press button");
        }
        

    }

    // just for danger sign blinking
    private IEnumerator Danger()
    {
        while(!_btnPressed)
        {
            _inputText.text = randKey.ToString();
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
                    // Bubble pops here
                    _chewingScore = 0f;
                    // Save score here a
                    // Lose Condition
                    StopAllCoroutines();
                    _dangerSign.SetActive(false);
                }

            }
            else
            {   
                // Resets colour and burst timer
                _dangerSign_img.color = Color.Lerp(_endColour, _startColour, _burstDuration);
                _burstTimer = 0f;
            }
        }
        ;
    }


    //Random key 
    KeyCode RandomKey()
    {
        KeyCode key = KeyCode.None;

        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                key = KeyCode.W;
                break;
            case 1:
                key = KeyCode.A;
                break;
            case 2:
                key = KeyCode.S;
                break;
            case 3:
                key = KeyCode.D;
                break;
        }

        Debug.Log(key.ToString());
        //spawnBubble = true;
        //popBubble = false;

        return key;
    }

}
