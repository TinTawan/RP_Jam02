using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManChomp : MonoBehaviour
{
    [SerializeField] GameObject topTeeth, bottomTeeth;
    [SerializeField] Transform topMoveTo, bottomMoveTo;

    Vector2 topOrigin, bottomOrigin;

    [SerializeField] float chompTime = 0.25f;

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
        if (Input.GetKeyDown(KeyCode.W))
        {
            biting = true;
        }

        if (biting)
        {
            ChompDown();
        }
    }

    void ChompDown()
    {
        topTeeth.transform.position = Vector2.Lerp(topTeeth.transform.position, topMoveTo.position, /*Time.deltaTime * */chompTime);
        bottomTeeth.transform.position = Vector2.Lerp(bottomTeeth.transform.position, bottomMoveTo.position, /*Time.deltaTime * */chompTime);

        StartCoroutine(Bite());

    }

    IEnumerator Bite()
    {
        //topTeeth.transform.position = topMoveTo.position;
        //bottomTeeth.transform.position = bottomMoveTo.position;

        //topTeeth.transform.position = Vector2.Lerp(topOrigin, topMoveTo.position, Time.deltaTime * chompTime);
        //bottomTeeth.transform.position = Vector2.Lerp(bottomOrigin, bottomMoveTo.position, Time.deltaTime * chompTime);



        yield return new WaitForSeconds(chompTime);

        biting = false;

        topTeeth.transform.position = topOrigin;
        bottomTeeth.transform.position = bottomOrigin;

    }
}
