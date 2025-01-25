using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KeyPressMinigame : MonoBehaviour
{
    //Chewing Minigame 
    float chewTimer = 10f;
    float countDown = 0f;

    float score = 0;

    bool chewing = false, gameStart = false;

    KeyCode randKey;

    //UI Section
    [SerializeField] Slider countdownSlider;
    [SerializeField] TextMeshProUGUI keyText, scoreText;

    private void Start()
    {
        //StartChewGame();

        StartCoroutine(StartGame());
    }

    private void Update()
    {
        if (gameStart)
        {
            GenerateRandomKey();
            countdownSlider.value = countDown;
        }

    }

    /*void StartChewGame()
    {
        chewing = true;
        randKey = RandomKey();
        countDown = chewTimer;

        keyText.text = "";
    }*/

    IEnumerator StartGame()
    {
        keyText.text = "";

        scoreText.text = "3";
        yield return new WaitForSeconds(1);
        scoreText.text = "2";
        yield return new WaitForSeconds(1);
        scoreText.text = "1";
        yield return new WaitForSeconds(1);

        chewing = true;
        gameStart = true;
        randKey = RandomKey();
        countDown = chewTimer;

    }

    /*void EndChewGame()
    {
        chewing = false;
        scoreText.text = $"Final score: {score}";
        countDown = 0;

        keyText.text = "";

        //save score to manager and move to next scene
    }*/
    IEnumerator EndChewGame()
    {
        chewing = false;
        scoreText.text = $"Final score: {score}";
        countDown = 0;

        keyText.text = "";

        //save score to manager
        Debug.Log("Times Up");
        yield return new WaitForSeconds(1);
        keyText.transform.position = Vector3.zero;
        keyText.text = "Press any key ...";

        //move to next scene
        if (Input.anyKeyDown)
        {


            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }

    }

    void GenerateRandomKey()
    {
        if(countDown > 0f )
        {
            countDown -= Time.deltaTime;

            keyText.text = randKey.ToString();
            scoreText.text = $"Score: {score}";

            //Debug.Log($"Time Left: {System.Math.Round(countDown,2)}");

            /*if (Input.GetKeyDown(randKey))
            {
                score++;

                randKey = RandomKey();

                Debug.Log($"Score: {score}");
            }*/
            /*else if(!Input.GetKeyDown(randKey))
            {
                score -= 0.5f;
                Debug.Log($"Wrong button lose points: {score}");

            }*/

            //check for any button key press to see if the correct or incorrect button is pressed
            foreach (KeyCode pressedKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(pressedKey))
                {
                    if (pressedKey == randKey)
                    {
                        //correct key pressed
                        score++;

                        randKey = RandomKey();

                        Debug.Log($"Score: {score}");

                        keyText.transform.position = new Vector2(transform.position.x + Random.Range(-2,2), transform.position.y + Random.Range(-2, 2));
                        
                    }
                    else
                    {
                        //wrong key pressed
                        score -= 0.5f;

                        randKey = RandomKey();

                        Debug.Log($"Wrong button lose points: {score}");

                        keyText.transform.position = new Vector2(transform.position.x + Random.Range(-2, 2), transform.position.y + Random.Range(-2, 2));
                    }
                }
            }

        }
        else
        {
            //timer ends so save info and move scene
            StartCoroutine(EndChewGame());

        }
    }



    //generate a random key from WASD and return it
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
}
