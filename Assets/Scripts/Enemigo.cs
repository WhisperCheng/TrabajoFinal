using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public int vidaEnemigo;
    public float distanciaEnemigoPersonaje;
    public GameObject personajeObjetivo;
    bool limiteDistancia;
    NavMeshAgent agent;
    

    // Start is called before the first frame update
    void Start()
    {
        vidaEnemigo = 100;
        personajeObjetivo = GameObject.Find("Personaje");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distanciaEnemigoPersonaje = Vector3.Distance(agent.transform.position, personajeObjetivo.transform.position);

        if (distanciaEnemigoPersonaje >= 15 && limiteDistancia == false) 
        {
            agent.destination = personajeObjetivo.transform.position;
        }
        else if (distanciaEnemigoPersonaje < 15) 
        {
            agent.destination = agent.transform.position;
            limiteDistancia = true;
        }
    }
    public void hitDaño(int dañoArma)
    {
        vidaEnemigo -= dañoArma;
        Debug.Log("Enemigo: Eh recibido daño");
        if (vidaEnemigo <= 0)
        {
            Destroy(gameObject);
        }
    }
}
