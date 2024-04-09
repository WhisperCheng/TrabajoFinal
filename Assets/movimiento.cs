using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class movimiento : MonoBehaviour
{
    public float velocidad;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        velocidad = 10;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float movH = Input.GetAxis("Horizontal");
        float movV = Input.GetAxis("Vertical");
        rb.velocity = new Vector3 (movH * velocidad , rb.velocity.y, movV * velocidad);
    }
}
