using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _winOrLoseText;
    [SerializeField] private TextMeshProUGUI _finalScoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        bool gameWon = GameManager.GetHasWon(); 
        Debug.Log("End screen bool:" + gameWon);
        WinOrLose(gameWon);    
    }


    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        
    }

    void WinOrLose(bool hasWon)
    {
        if (hasWon)
        {
            _winOrLoseText.text = "You Win!";
           
        }
        else 
        {
           _winOrLoseText.text = "You Lose";
        }
    }

    void UpdateScore()
    {
        float final_score = GameManager.GetBlowScore();
        
        _finalScoreText.text = $"Score: {(int)final_score}";
    }

    
    public void Retry()
    {
        // Load chweing scene 
        Debug.Log("Loading chewing scene :)");
    }

    public void MainMenu()
    {
        // Load Menu scene 
        Debug.Log("Loading Main Menu scene :)");
    }

    
    public void Quit()
    {
        Application.Quit();
        Debug.Log("quit game :D");
    }

}
