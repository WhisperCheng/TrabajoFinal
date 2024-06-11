using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionCamara : MonoBehaviour
{
    public GameObject objetivo;
    public float velocidad;
    // Start is called before the first frame update
    void Start()
    {
        velocidad = 15;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(objetivo.transform.position);
        transform.RotateAround(objetivo.transform.position, Vector3.up, velocidad * Time.deltaTime);
    }
}
