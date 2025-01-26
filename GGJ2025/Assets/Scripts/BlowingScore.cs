using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlowingScore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _bubble;
    [SerializeField] private GameObject _dangerSign;
    [SerializeField] private Animator dangerAnim, gumAnim;

    [Header("UI References")]
    [SerializeField] private Slider _timerSlider;
    [SerializeField] private TextMeshProUGUI _scoreText, countdownText;
    [SerializeField] private TextMeshProUGUI _inputText;

    [Header("Values")]
    [SerializeField] private float blowRate;
    [SerializeField] private float _btnPressTimer;
    //[SerializeField] private float _signTimer;
    [SerializeField] private float _burstDuration;  
    [SerializeField] private Color _startColour;
    [SerializeField] private Color _endColour;

    private float _chewingScore;
    private int _blowScore;
    private bool _btnPressed = false;
    private Image _dangerSign_img;

    private float _currentBtnTimer;
    private float _burstTimer;

    //Random key input section
    KeyCode randKey;

    //countdown start game
    bool gameStart = false;

    bool _gameEnd = false; // Checks for game to stop (weither win or lose)
    bool _gameWon = false; // Checks if the game is won or lost

    //Bubble moving particle
    ParticleSystem ps;
    ParticleSystem.Particle[] particles;

    

    private void Awake()
    {
        _chewingScore = GameManager.GetChewScore();
        _timerSlider.maxValue = _chewingScore;
        _timerSlider.value = _timerSlider.maxValue;

        _dangerSign_img = _dangerSign.GetComponent<Image>();

        ps = FindObjectOfType<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGame());

        ps.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            BlowingTimer();
            _blowScore = (int)System.Math.Round(_bubble.transform.localScale.x * 100, 2);
            _scoreText.text = $"Score: {_blowScore}";
        }
        else
        {
            ps.Clear();
        }

        // Check to change scene
        if (_gameEnd && _gameWon)
        {
            // Debug.Log("Win");
            StartCoroutine(EndGame(_gameWon));
        }
        if (_gameEnd && !_gameWon)
        {
            // Debug.Log("Lose");
            StartCoroutine(EndGame(_gameWon));
        }


        particles = new ParticleSystem.Particle[ps.main.maxParticles];

        int count = ps.GetParticles(particles);
        if (count > 0)
        {
            _dangerSign.transform.position = ps.transform.TransformPoint(particles[0].position);

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
        if (!_gameEnd)
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

                    StartCoroutine(HideDangerBubble());

                    _burstTimer = 0f;   

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
                else
                {
                    DecreaseScale();
                }
                if(Input.GetKeyDown(KeyCode.Space)){
                    AudioManager.instance.PlayMusic("Blow");
                }
                if(Input.GetKeyUp(KeyCode.Space)){
                    AudioManager.instance.StopMusic();
                }

            }
            else
            {
                _gameWon = true;
                _gameEnd = true;
                _dangerSign.SetActive(false);
            }  
        }

    }

    IEnumerator HideDangerBubble()
    {
        dangerAnim.SetBool("popped", true);
        ps.Stop();

        yield return new WaitForSeconds(0.25f);

        _dangerSign.SetActive(false);
        ps.Clear();
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
        
        dangerAnim.SetBool("popped", false);
        ps.Play();

        _inputText.text = randKey.ToString();
        _dangerSign.SetActive(true);
        yield return null;


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
                    gumAnim.SetBool("popped", true);


                    Debug.Log("LOSER!!!");
                    // Bubble pops here
                    _gameWon = false;
                    _gameEnd = true;
                    _chewingScore = 0f;
                    // Lose Condition
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

        return key;
    }

    private IEnumerator EndGame(bool won)
    {   
        // Save Score
        GameManager.SetBlowScore(_blowScore);
        // Won or lose
        // Debug.Log("Blowing screen bool "+won);
        GameManager.SetHasWon(won);
        // wait a secc
        yield return new WaitForSeconds(1f);
        // move to next scene
        string next_scene = "EndGame";
        SceneManager.LoadScene(next_scene);

    }

    

}
