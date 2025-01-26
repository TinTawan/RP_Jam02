using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManChomp : MonoBehaviour
{
    [Header("Mouth Chomping")]
    [SerializeField] KeyPressMinigame kpMinigame;

    [SerializeField] GameObject topTeeth, bottomTeeth;
    [SerializeField] Transform topMoveTo, bottomMoveTo, tLeftMoveTo, tRightMoveTo, bLeftMoveTo, bRightMoveTo;

    Vector3 topOrigin, bottomOrigin;

    [SerializeField] float chompTime = 0.25f;

    bool biteDown, biteUp, biteLeft, biteRight;

    [Header("Eye Movement")]
    [SerializeField] GameObject lEye;
    [SerializeField] GameObject rEye;

    [SerializeField] Vector4 lEyeRange, rEyeRange;
    Vector2 eyeMoveTimer;



    private void Start()
    {

        topOrigin = topTeeth.transform.position;
        bottomOrigin = bottomTeeth.transform.position;

        eyeMoveTimer.x = Random.value * 1.5f;
        eyeMoveTimer.y = Random.value * 1.5f;

    }

    private void Update()
    {
        if (kpMinigame.GetGameStart())
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                biteUp = true;

            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                biteDown = true;

            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                biteLeft = true;

            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                biteRight = true;

            }

            EyesLook();
        }
        

        if (biteDown)
        {
            ChompDown();
        }
        if (biteUp)
        {
            ChompUp();
        }
        if (biteLeft)
        {
            ChompLeft();
        }
        if (biteRight)
        {
            ChompRight();
        }
    }

    

    void ChompUp()
    {
        bottomTeeth.transform.position = Vector3.Lerp(bottomTeeth.transform.position, bottomMoveTo.position, chompTime);

        StartCoroutine(ResetChomp());

    }


    void ChompDown()
    {
        topTeeth.transform.position = Vector3.Lerp(topTeeth.transform.position, topMoveTo.position, chompTime);

        StartCoroutine(ResetChomp());

    }


    void ChompLeft()
    {
        topTeeth.transform.position = Vector3.Lerp(topTeeth.transform.position, tLeftMoveTo.position, chompTime);
        bottomTeeth.transform.position = Vector3.Lerp(bottomTeeth.transform.position, bRightMoveTo.position, chompTime);

        StartCoroutine(ResetChomp());
    }

    void ChompRight()
    {
        topTeeth.transform.position = Vector3.Lerp(topTeeth.transform.position, tRightMoveTo.position, chompTime);
        bottomTeeth.transform.position = Vector3.Lerp(bottomTeeth.transform.position, bLeftMoveTo.position, chompTime);

        StartCoroutine(ResetChomp());
    }

    IEnumerator ResetChomp()
    {
        yield return new WaitForSeconds(chompTime);

        biteDown = false;
        biteUp = false;
        biteLeft = false;
        biteRight = false;

        topTeeth.transform.position = topOrigin;
        bottomTeeth.transform.position = bottomOrigin;

    }


    //Crazy eyes
    Vector2 RandomEyeRotation(Vector4 eyeRange)
    {
        return new Vector2(Random.Range(eyeRange.x, eyeRange.y), Random.Range(eyeRange.z, eyeRange.w));

    }


    void EyesLook()
    {
        eyeMoveTimer.x -= Time.deltaTime;
        eyeMoveTimer.y -= Time.deltaTime;
        if (eyeMoveTimer.x <= 0)
        {
            rEye.transform.eulerAngles = new(RandomEyeRotation(rEyeRange).x, RandomEyeRotation(rEyeRange).y, 0);

            eyeMoveTimer.x = Random.value * 1.5f;

        }
        if (eyeMoveTimer.y <= 0)
        {
            lEye.transform.eulerAngles = new(RandomEyeRotation(lEyeRange).x, RandomEyeRotation(lEyeRange).y, 0);

            eyeMoveTimer.y = Random.value * 1.5f;

        }


    }
}
