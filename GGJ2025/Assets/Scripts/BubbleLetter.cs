using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BubbleLetter : MonoBehaviour
{
    TextMeshProUGUI keyText;
    KeyPressMinigame kpMiniGame;

    Animator anim;

    ParticleSystem ps;
    ParticleSystem.Particle[] particles;

    private void Start()
    {
        kpMiniGame = FindObjectOfType<KeyPressMinigame>();
        anim = GetComponent<Animator>();

        keyText = GetComponentInChildren<TextMeshProUGUI>();
        keyText.text = "";

        ps = FindObjectOfType<ParticleSystem>();

        ps.Pause();
    }

    private void Update()
    {
        if (kpMiniGame.GetGameStart())
        {
            keyText.text = kpMiniGame.GetKeyText();

        }
        else
        {
            ps.Clear();
            Destroy(gameObject);
        }

        BubblePop();

        particles = new ParticleSystem.Particle[ps.main.maxParticles];

        int count = ps.GetParticles(particles);
        if (count > 0)
        {
            transform.position = ps.transform.TransformPoint(particles[0].position);

        }

    }


    void BubblePop()
    {
        if (kpMiniGame.GetPopBubble())
        {
            anim.SetBool("popped", true);
            ps.Stop();
            //ps.Clear();
            //ps.Pause();
        }
        else
        {
            anim.SetBool("popped", false);
            ps.Play();

        }
    }

    private void OnDestroy()
    {
        kpMiniGame.SetPopBubble(false);

        ps.Clear();

    }
}
