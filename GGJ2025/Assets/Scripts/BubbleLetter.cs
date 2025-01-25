using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BubbleLetter : MonoBehaviour
{
    TextMeshProUGUI keyText;
    KeyPressMinigame kpMiniGame;

    Animator anim;


    private void Start()
    {
        kpMiniGame = FindObjectOfType<KeyPressMinigame>();
        anim = GetComponent<Animator>();

        keyText = GetComponentInChildren<TextMeshProUGUI>();
        keyText.text = "";


    }

    private void Update()
    {
        if (kpMiniGame.GetGameStart())
        {
            keyText.text = kpMiniGame.GetKeyText();

        }

        BubblePop();
    }


    void BubblePop()
    {
        if (kpMiniGame.GetPopBubble())
        {
            anim.SetBool("popped", true);
        }
        else
        {
            anim.SetBool("popped", false);

        }
    }

    private void OnDestroy()
    {
        kpMiniGame.SetPopBubble(false);

    }
}
