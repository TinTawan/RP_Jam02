using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManChomp : MonoBehaviour
{
    [SerializeField] GameObject topTeeth, bottomTeeth;
    [SerializeField] Transform topMoveTo, bottomMoveTo;

    Vector2 topOrigin, bottomOrigin;

    [SerializeField] float chompTime = 0.25f;

    bool biteDown, biteUp, biteLeft, biteRight;

    private void Start()
    {
        //topOrigin = new(0, -2.3f);
        //bottomOrigin = new(0, -4.15f);

        topOrigin = topTeeth.transform.position;
        bottomOrigin = bottomTeeth.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            biteUp = true;

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            biteDown = true;

        }

        if (biteDown)
        {
            ChompDown();
        }
        if (biteUp)
        {
            ChompUp();
        }
    }

    

    void ChompUp()
    {
        bottomTeeth.transform.position = Vector2.Lerp(bottomTeeth.transform.position, bottomMoveTo.position, chompTime);

        StartCoroutine(ResetChompUp());

    }

    IEnumerator ResetChompUp()
    {
        yield return new WaitForSeconds(chompTime);

        biteUp = false;

        bottomTeeth.transform.position = bottomOrigin;
    }


    void ChompDown()
    {
        topTeeth.transform.position = Vector2.Lerp(topTeeth.transform.position, topMoveTo.position, chompTime);

        StartCoroutine(ResetChompDown());

    }
    IEnumerator ResetChompDown()
    {
        yield return new WaitForSeconds(chompTime);

        biteDown = false;

        topTeeth.transform.position = topOrigin;

    }
}
