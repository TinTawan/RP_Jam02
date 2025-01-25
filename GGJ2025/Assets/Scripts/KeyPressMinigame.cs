using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPressMinigame : MonoBehaviour
{
    float chewTimer = 10f;
    float countDown = 0f;

    float score = 0;

    bool chewing = false;

    KeyCode randKey;

    private void Start()
    {
        StartChewGame();
    }

    private void Update()
    {
        GenerateRandomKey();
    }

    void StartChewGame()
    {
        chewing = true;
        randKey = RandomKey();
        countDown = chewTimer;
    }

    void EndChewGame()
    {
        chewing = false;
        Debug.Log($"Final score: {score}");
        countDown = chewTimer;
    }

    void GenerateRandomKey()
    {
        if(countDown > 0f && chewing)
        {
            countDown -= Time.deltaTime;
            //Debug.Log($"Time Left: {System.Math.Round(countDown,2)}");

            if (Input.GetKeyDown(randKey))
            {
                score++;

                randKey = RandomKey();

                Debug.Log($"Score: {score}");
            }
            /*else if(!Input.GetKeyDown(randKey))
            {
                score -= 0.5f;
                Debug.Log($"Wrong button lose points: {score}");

            }*/
        }
        else
        {
            //timer ends so save info and move scene
            Debug.Log("Times Up");

            EndChewGame();

        }
    }




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
