using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BubbleLetter : MonoBehaviour
{
    TextMeshProUGUI keyText;
    KeyPressMinigame kpMiniGame;

    private void Start()
    {
        kpMiniGame = FindObjectOfType<KeyPressMinigame>();

        keyText = GetComponentInChildren<TextMeshProUGUI>();
        keyText.text = "";
    }

    private void Update()
    {
        if (kpMiniGame.GetGameStart())
        {
            keyText.text = kpMiniGame.GetKeyText();

        }


    }

}
