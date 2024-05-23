using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    //Debo comprobar todo esto en clase que tengo los proyectos correspondientes

    public int vidaEnemigo;
    float fuerzagravedad;
    Vector3 gravedadVector;
    CharacterController characterController;
    public GameObject personajeObjetivo;

    // Start is called before the first frame update
    void Start()
    {
        vidaEnemigo = 100;
        fuerzagravedad = -9.81f;
        characterController = GetComponent<CharacterController>();
        personajeObjetivo = GameObject.Find("Personaje");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gravedadVector.y += fuerzagravedad * Time.deltaTime;

        characterController.Move(gravedadVector * Time.deltaTime);
    }
    public void hitDaño(int dañoArma)
    {
        vidaEnemigo -= dañoArma;
        Debug.Log("Enemigo: Eh recibido da�o");
        if (vidaEnemigo <= 0)
        {
            Destroy(gameObject);
        }
    }
}
