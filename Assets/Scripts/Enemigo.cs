using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    //Variables int
    public int vidaEnemigo;
    public int mascaraEstructuras;

    //Variables float
    public float distanciaEnemigoPersonaje;
    public float delay;
    public float ultimoDisparo;
    public float distanciaRadio;
    public float probabilidad;
    //Variables bool
    public bool visionDirecta;
    public bool quieto;
    public bool desactivado;

    //Variables Gameobject
    public GameObject personajeObjetivo;
    public GameObject balaPrefab;
    public GameObject efectoMuerte;
    public GameObject efectoMuerteBorrar;

    //Lista de GameObject
    public GameObject[] objetosHabilidades;

    //Variables transform
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform visionSalida;

    //Declaraciones de script
    NavMeshAgent agent;
    Animator animator;
    public SpawnManager spaManager;

    // Start is called before the first frame update
    void Start()
    {
        vidaEnemigo = 100;
        delay = 1;
        distanciaRadio = 0.4f;
        ultimoDisparo = Time.time;
        personajeObjetivo = GameObject.Find("Personaje");
        spaManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        efectoMuerte = Resources.Load<GameObject>("PlasmaExplosionEffect");
        mascaraEstructuras = 1 << 6;
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
        vision();
    }

    //Se encarga del movimiento mediante el agente y el desactivar en caso de que reciba daño con la granda PEM
    public void movimiento()
    {
        if (desactivado == false) 
        { 
            distanciaEnemigoPersonaje = Vector3.Distance(agent.transform.position, personajeObjetivo.transform.position);

            if (quieto == false && (distanciaEnemigoPersonaje >= 20 || visionDirecta == false))
            {
                agent.destination = personajeObjetivo.transform.position;
                animator.SetBool("Caminando", true);
                animator.SetBool("Disparando", false);
            }
            else if (quieto == true || (distanciaEnemigoPersonaje < 20 && visionDirecta == true))
            {
                quieto = true;
                agent.destination = agent.transform.position;
                transform.LookAt(personajeObjetivo.transform.position);
                animator.SetBool("Caminando", false);
                animator.SetBool("Disparando", true);
                dispararSpawnPoint1();
            }
        }
        else if(desactivado == true) 
        {
            agent.destination = agent.transform.position;
            animator.SetBool("EfectoPEM", true);
            animator.SetBool("Disparando", false);
            animator.SetBool("Caminando", false);
            Invoke("cooldownDesactivado", 3);
        }
    }

    //Se encarga de los datos relacionados con el recibir daño
    public void hitDaño(int dañoArma)
    {
        vidaEnemigo -= dañoArma;
        if (vidaEnemigo <= 0)
        {
            soltarObjeto();
            GameManager.Instance.enemigosEliminados++;
            spaManager.enemigosRestantes--;
            spaManager.comprobarNumeroEnemigos();
            InterfazManager.Instance.interfazPantallas();
            efectoMuerteBorrar = Instantiate(efectoMuerte, transform.position, Quaternion.identity);
            Destroy(efectoMuerteBorrar, 1);
            Destroy(gameObject);
        }
    }

    //Se encarga de ver si suelta un objeto o no todo de forma aleatoria
    public void soltarObjeto()
    {
        probabilidad = Random.Range(0, 100);
        Debug.Log(probabilidad);
        if (probabilidad > 50)
        {
            Instantiate(objetosHabilidades[Random.Range(0, 6)], transform.position, Quaternion.identity);
        }
    }

    //Se encarga del cooldown de desactivar al personaje
    public void cooldownDesactivado()
    {
        if (desactivado == true)
        {
            desactivado = false;
            animator.SetBool("EfectoPEM", false);
        }
    }

    //Se encarga del spawn de los disparos en el spawn1
    public void dispararSpawnPoint1()
    {
        if (Time.time - ultimoDisparo > delay)
        {
            Instantiate(balaPrefab, spawnPoint1.position, transform.rotation);
            Invoke("dispararSpawnPoint2", 0.5f);
            ultimoDisparo = Time.time;
        }
    }

    //Se encarga del spawn de los disparos en el spawn2
    public void dispararSpawnPoint2()
    {
        transform.LookAt(personajeObjetivo.transform.position);
        Instantiate(balaPrefab, spawnPoint2.position, transform.rotation);
    }

    //Se encarga de ver si el enemigo tiene vision directa con el personaje
    public void vision()
    {
        RaycastHit[] hits = Physics.SphereCastAll(visionSalida.position, distanciaRadio, visionSalida.forward, Mathf.Infinity, mascaraEstructuras);
        visionSalida.LookAt(personajeObjetivo.transform.position);
        if (hits.Length == 0)
        {
            visionDirecta = true;
        }
        else if (hits.Length > 0) 
        {
            visionDirecta = false;
            quieto = false;
        }
        Debug.Log(hits.Length); 
    }
}
