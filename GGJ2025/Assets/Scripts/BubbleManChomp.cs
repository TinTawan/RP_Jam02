using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManChomp : MonoBehaviour
{
    [SerializeField] GameObject topTeeth, bottomTeeth;
    [SerializeField] Transform topMoveTo, bottomMoveTo;

    Vector2 topOrigin, bottomOrigin;

    bool biting;

    private void Start()
    {
        //topOrigin = new(0, -2.3f);
        //bottomOrigin = new(0, -4.15f);

        topOrigin = topTeeth.transform.position;
        bottomOrigin = bottomTeeth.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            biting = true;
        }

        if (biting)
        {
            StartCoroutine(Bite());
        }
    }

    IEnumerator Bite()
    {
        topTeeth.transform.position = topMoveTo.position;
        bottomTeeth.transform.position = bottomMoveTo.position;

        biting = false;

        yield return new WaitForSeconds(0.2f);

        topTeeth.transform.position = topOrigin;
        bottomTeeth.transform.position = bottomOrigin;



    }
}
