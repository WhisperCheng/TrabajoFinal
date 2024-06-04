using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public int vidaEnemigo;
    public float distanciaEnemigoPersonaje;
    public GameObject personajeObjetivo;
    public bool limiteDistancia;
    public bool desactivado;
    NavMeshAgent agent;
    

    // Start is called before the first frame update
    void Start()
    {
        vidaEnemigo = 100;
        personajeObjetivo = GameObject.Find("Personaje");
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        Debug.Log(desactivado);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (desactivado == false)
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
        else if (distanciaEnemigoPersonaje < 15 || desactivado == true)
        {
            agent.destination = agent.transform.position;
            Invoke("cooldownDesactivado", 3);
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
    public void cooldownDesactivado()
    {
        if (desactivado == true)
        {
            desactivado = false;
        }
    }
}
