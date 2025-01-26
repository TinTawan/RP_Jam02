using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumChew : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float force = 5f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (Input.anyKeyDown)
        {
            Vector2 dir = new(Random.Range(-1,1),Random.Range(0,1));
            MoveGum(dir);
        }
    }

    void MoveGum(Vector2 dir)
    {
        rb.AddForce(dir * Random.value * force, ForceMode.Impulse);
    }

    
}
