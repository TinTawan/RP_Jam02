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

    bool doThis = true;

    private void Start()
    {
        kpMiniGame = FindObjectOfType<KeyPressMinigame>();
        anim = GetComponent<Animator>();

        keyText = GetComponentInChildren<TextMeshProUGUI>();
        keyText.text = "";

        ps = FindObjectOfType<ParticleSystem>();

        //ps.Stop();
        ps.Pause();
    }

    private void Update()
    {
        if (kpMiniGame.GetGameStart())
        {
            keyText.text = kpMiniGame.GetKeyText();
            //ps.Play();
        }
        else
        {
            //ps.Clear();
            ps.Stop();
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
            //ps.Stop();
            ps.Clear();
            //ps.Pause();

            Debug.Log("pop bubble");

            doThis = true;
        }
        else
        {
            if (doThis)
            {
                anim.SetBool("popped", false);
                ps.Play();

                Debug.Log("reset bubble");
                kpMiniGame.SetPopBubble(false);

                doThis = false;
            }
            

        }
    }

    private void OnDestroy()
    {
        kpMiniGame.SetPopBubble(false);

        ps.Stop();
        //ps.Clear();

    }
}
