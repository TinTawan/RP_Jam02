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

    bool gameStart = false, spawnBubble = false, popBubble = false;

    KeyCode randKey;

    //UI Section
    [SerializeField] Slider countdownSlider;
    [SerializeField] TextMeshProUGUI scoreText;

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


    IEnumerator StartGame()
    {

        scoreText.text = "3";
        yield return new WaitForSeconds(1);
        scoreText.text = "2";
        yield return new WaitForSeconds(1);
        scoreText.text = "1";
        yield return new WaitForSeconds(1);

        gameStart = true;
        randKey = RandomKey();
        countDown = chewTimer;

    }


    IEnumerator EndChewGame()
    {
        gameStart = false;

        scoreText.text = $"Final score: {score}";
        countDown = 0;

        Debug.Log("Times Up");
        yield return new WaitForSeconds(1);


        //save score to manager
        GameManager.SetChewScore(score);

        //move to next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }

    void GenerateRandomKey()
    {
        if(countDown > 0f )
        {
            countDown -= Time.deltaTime;

            //keyText.text = randKey.ToString();
            scoreText.text = $"Score: {score}";


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

                        //keyText.transform.position = new Vector2(transform.position.x + Random.Range(-2,2), transform.position.y + Random.Range(-2, 2));

                        popBubble = true;

                    }
                    else
                    {
                        //wrong key pressed
                        if(score > 0)
                        {
                            score -= 0.5f;

                        }

                        randKey = RandomKey();

                        Debug.Log($"Wrong button lose points: {score}");

                        //keyText.transform.position = new Vector2(transform.position.x + Random.Range(-2, 2), transform.position.y + Random.Range(-2, 2));

                        popBubble = true;

                    }

                    //spawnBubble = false;

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
        spawnBubble = true;
        //popBubble = false;

        return key;
    }

    public bool GetGameStart()
    {
        return gameStart;
    }

    public bool GetSpawnBubble()
    {
        return spawnBubble;
    }
    public void SetSpawnBubble(bool inBool)
    {
        spawnBubble = inBool;
    }

    public bool GetPopBubble()
    {
        return popBubble;
    }
    public void SetPopBubble(bool inBool)
    {
        popBubble = inBool;
    }

    public string GetKeyText()
    {
        return randKey.ToString();
    }


}
