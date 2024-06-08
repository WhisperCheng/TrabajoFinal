using Cinemachine.Utility;
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
    public float delay;
    public float ultimoDisparo;
    public bool visionDirecta;
    public bool quieto;
    public GameObject efectoMuerte;
    public GameObject efectoMuerteBorrar;
    public int mascaraEstructuras;
    public float distanciaRadio;
    public Transform posicionVision;

    //Debo de mirar este script para limpiarlo un poco ya que esta horrible


    // Start is called before the first frame update
    void Start()
    {
        vidaEnemigo = 100;
        delay = 1;
        distanciaRadio = 0.5f;
        ultimoDisparo = Time.time;
        personajeObjetivo = GameObject.Find("Personaje");
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
    public void movimiento()
    {
        if (desactivado == false) 
        { 
            distanciaEnemigoPersonaje = Vector3.Distance(agent.transform.position, personajeObjetivo.transform.position);

            if (quieto == false && (distanciaEnemigoPersonaje >= 15 || visionDirecta == false))
            {
                agent.destination = personajeObjetivo.transform.position;
                animator.SetBool("Caminando", true);
                animator.SetBool("Disparando", false);
            }
            else if (quieto == true || (distanciaEnemigoPersonaje < 15 && visionDirecta == true))
            {
                quieto = true;
                agent.destination = agent.transform.position;
                posicionVision.LookAt(personajeObjetivo.transform.position);
                animator.SetBool("Caminando", false);
                animator.SetBool("Disparando", true);
                dispararSpawnPoint1();
            }
        }

        //Debo arreglar el rayo de vision del enemigo para que no dispare de forma loca

        else if(desactivado == true) 
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
            efectoMuerteBorrar = Instantiate(efectoMuerte, transform.position, Quaternion.identity);
            Destroy(efectoMuerteBorrar, 1);
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
        if (Time.time - ultimoDisparo > delay)
        {
            Instantiate(balaPrefab, spawnPoint1.position, transform.rotation);
            Invoke("dispararSpawnPoint2", 0.5f);
            ultimoDisparo = Time.time;
        }
    }
    public void dispararSpawnPoint2()
    {
        Instantiate(balaPrefab, spawnPoint2.position, transform.rotation);
    }
    public void vision()
    {
        RaycastHit[] hits = Physics.SphereCastAll(posicionVision.position, distanciaRadio, transform.forward, Mathf.Infinity, mascaraEstructuras);

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
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(posicionVision.position + transform.forward * 15, 1f);
    }
}
