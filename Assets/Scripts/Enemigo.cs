using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public int vidaEnemigo;
    public float distanciaEnemigoPersonaje;
    public GameObject personajeObjetivo;
    public bool desactivado;
    NavMeshAgent agent;
    Animator animator;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public GameObject balaPrefab;
    float delay;
    float lastShoot;
    bool disparando;


    //Debo de mirar este script para limpiarlo un poco ya que esta horrible


    // Start is called before the first frame update
    void Start()
    {
        vidaEnemigo = 100;
        delay = 1;
        lastShoot = Time.time;
        personajeObjetivo = GameObject.Find("Personaje");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movimiento();
    }
    public void movimiento()
    {
        if (desactivado == false)
        {
            distanciaEnemigoPersonaje = Vector3.Distance(agent.transform.position, personajeObjetivo.transform.position);

            if (distanciaEnemigoPersonaje >= 15 && disparando == false)
            {
                agent.destination = personajeObjetivo.transform.position;
                animator.SetBool("Caminando", true);
                animator.SetBool("Disparando", false);
            }
            else if (distanciaEnemigoPersonaje < 15 || disparando == true)
            {

                disparando = true;
                agent.destination = agent.transform.position;
                transform.LookAt(personajeObjetivo.transform.position);
                animator.SetBool("Caminando", false);
                animator.SetBool("Disparando", true);
                dispararSpawnPoint1();
            }
        }
        else if (desactivado == true)
        {
            agent.destination = agent.transform.position;
            animator.SetBool("EfectoPEM", true);
            animator.SetBool("Disparando", false);
            animator.SetBool("Caminando", false);
            Invoke("cooldownDesactivado", 3);
        }
    }
    public void hitDaño(int dañoArma)
    {
        vidaEnemigo -= dañoArma;
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
            animator.SetBool("EfectoPEM", false);
        }
    }
    public void dispararSpawnPoint1()
    {
        if (Time.time - lastShoot > delay)
        {
            Instantiate(balaPrefab, spawnPoint1.position, transform.rotation);
            Invoke("dispararSpawnPoint2", 0.5f);
            lastShoot = Time.time;
        }
    }
    public void dispararSpawnPoint2()
    {
        Instantiate(balaPrefab, spawnPoint2.position, transform.rotation);
    }
}
