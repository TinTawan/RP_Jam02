using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManChomp : MonoBehaviour
{
    [SerializeField] KeyPressMinigame kpMinigame;

    [SerializeField] GameObject topTeeth, bottomTeeth;
    [SerializeField] Transform topMoveTo, bottomMoveTo, tLeftMoveTo, tRightMoveTo, bLeftMoveTo, bRightMoveTo;

    Vector2 topOrigin, bottomOrigin;

    [SerializeField] float chompTime = 0.25f;

    bool biteDown, biteUp, biteLeft, biteRight;

    private void Start()
    {

        topOrigin = topTeeth.transform.position;
        bottomOrigin = bottomTeeth.transform.position;
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
        bottomTeeth.transform.position = Vector2.Lerp(bottomTeeth.transform.position, bottomMoveTo.position, chompTime);

        StartCoroutine(ResetChomp());

    }

    /*IEnumerator ResetChompUp()
    {
        yield return new WaitForSeconds(chompTime);

        biteUp = false;

        bottomTeeth.transform.position = bottomOrigin;
    }*/


    void ChompDown()
    {
        topTeeth.transform.position = Vector2.Lerp(topTeeth.transform.position, topMoveTo.position, chompTime);

        StartCoroutine(ResetChomp());

    }
    /*IEnumerator ResetChompDown()
    {
        yield return new WaitForSeconds(chompTime);

        biteDown = false;

        topTeeth.transform.position = topOrigin;

    }*/

    void ChompLeft()
    {
        topTeeth.transform.position = Vector2.Lerp(topTeeth.transform.position, tLeftMoveTo.position, chompTime);
        bottomTeeth.transform.position = Vector2.Lerp(bottomTeeth.transform.position, bRightMoveTo.position, chompTime);

        StartCoroutine(ResetChomp());
    }

    void ChompRight()
    {
        topTeeth.transform.position = Vector2.Lerp(topTeeth.transform.position, tRightMoveTo.position, chompTime);
        bottomTeeth.transform.position = Vector2.Lerp(bottomTeeth.transform.position, bLeftMoveTo.position, chompTime);

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
}
