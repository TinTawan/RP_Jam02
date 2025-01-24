using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPressMinigame : MonoBehaviour
{
    float chewTimer = 10f;
    float countDown = 0f;

    int score = 0;

    private void Start()
    {
        countDown = chewTimer;
    }

    private void Update()
    {
        GenerateRandomKey();
    }

    void GenerateRandomKey()
    {
        if(countDown > 0f)
        {
            countDown -= Time.deltaTime;
            //Debug.Log($"Time Left: {System.Math.Round(countDown,2)}");

            /*if(Input.GetKey(RandomKey()))
            {
                score++;

                Debug.Log($"Score: {score}");
            }*/
        }
        else
        {
            //timer ends so save info and move scene
            Debug.Log("Times Up");

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
