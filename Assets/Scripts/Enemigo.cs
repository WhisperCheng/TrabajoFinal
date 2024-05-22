using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int vidaEnemigo;
    float fuerzagravedad;
    Vector3 gravedadVector;
    CharacterController characterController;
    
    public transform personajeObjetivo;

    // Start is called before the first frame update
    void Start()
    {
        personajeObjetivo = GameObject.Find("Personaje");
        vidaEnemigo = 100;
        fuerzagravedad = -9.81f;
        characterController = GetComponent<CharacterController>();
        agente = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gravedadVector.y += fuerzagravedad * Time.deltaTime;

        characterController.Move(gravedadVector * Time.deltaTime);
    }
    public void hitDa�o(int da�oArma)
    {
        vidaEnemigo -= da�oArma;
        Debug.Log("Enemigo: Eh recibido da�o");
        if (vidaEnemigo <= 0)
        {
            Destroy(gameObject);
        }
    }
}
