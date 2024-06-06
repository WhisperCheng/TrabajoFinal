using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public abstract class ArmasDatos : MonoBehaviour
{
    //Declaracion necesaria para las animacion de las armas
    public Animator animator;

    //Variables de datos generales que tendran las armas
    public int da�o;

    //Variable necesaria que tendran todas las armas siempre con el mismo valor
    public float rango;

    //Variables int para la capacidad del cargador de las armas y el poder saber las balas para recargar
    public int cargador;
    public int balasRestantes;

    //Booleanos simples para saber si el arma es automatica o no y saber si el arma puede disparar o no
    public bool armaAutomatica;
    public bool dispararPermitido;
    public bool cambiarArma;

    //Declaraciones para la posicion de las armas al ser recogidas y la posicion de la camara para el disparo
    public GameObject fpsCamera;
    public GameObject armaHolster;
    public GameObject impactoBala;
    public GameObject impactoBorrar;

    //Variables en prueba para el sway del arma y el tilt para el movimiento
    public float intensidadRotacion;
    public float smoothRotacion;

    //Variable quaternion para el origen de la rotacion
    public Quaternion rotacionOrigen;

    //Declaracion del script para conseguir el movimiento del raton
    public MovimientoCamara Vector2moveCamera;

    //Sistema de particulas para hacer el flash del ca�on del arma, faltaria ponerle un particula buena
    //Se debe de asignar el flash del arma desde la interfaz ya que no se como hacerlo mediante codigo
    [SerializeField] public ParticleSystem muzzleFlash;

    protected void Start()
    {
        fpsCamera = GameObject.Find("CamaraPrimeraPersona");
        armaHolster = GameObject.Find("Arma");
        Vector2moveCamera = GameObject.Find("CamaraPrimeraPersona").GetComponent<MovimientoCamara>();
        impactoBala = Resources.Load<GameObject>("EfectoImpactoPiedra");
        dispararPermitido = true;
        cambiarArma = true;
        intensidadRotacion = 5;
        smoothRotacion = 5;
        rango = Mathf.Infinity;
        rotacionOrigen = transform.localRotation;
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    //Se encarga del disparo del arma
    public abstract void Disparar();

    //Se encarga del balanceo del arma cuando no se esta ejecutando ninguna animacion
    public void balanceoArma()
    {
        if (dispararPermitido == true)
        {
            Quaternion t_adj_x = Quaternion.AngleAxis(intensidadRotacion * Vector2moveCamera.inputMovimientoCamara.x, Vector3.up);
            Quaternion t_adj_y = Quaternion.AngleAxis(intensidadRotacion * Vector2moveCamera.inputMovimientoCamara.y, Vector3.right);
            Quaternion target_rotation = rotacionOrigen * t_adj_x * t_adj_y;

            transform.localRotation = Quaternion.Lerp(transform.localRotation, target_rotation, Time.deltaTime * smoothRotacion);
        }
    }

    //Se encarga de la animacion de la recarga y el escalado de balas correspondiente
    public void recargando()
    {
        //Se tiene que a�adir esta condicion ya que podias recargar a la vez que disparabas
        //PERO SIN LA ANIMACION DE RECARGA
        if (animator.enabled == false || balasRestantes == 0)
        {
            animator.enabled = true;
            animator.SetTrigger("Recargando");
            animator.SetBool("SinBalas", false);
            dispararPermitido = false;
            cambiarArma = false;
            balasRestantes = cargador;
        }
    }

    //Se encarga de los datos relacionados con el disparo
    public void datosDisparo()
    {
        if (balasRestantes > 0)
        {
            Disparar();
            muzzleFlash.Play();
            dispararPermitido = false;
            cambiarArma = false;
            animator.enabled = true;
            animator.SetTrigger("Disparando");
            balasRestantes--;
        }
    }

    //Sirve para cambiar el estado de las armas cuando se quedan sin balas
    //Se tuvo que a�adir el "animator.enabled == false" por un bug del subfusil por ser automatica
    public void Sinbalas()
    {
        if (balasRestantes == 0)
        {
            animator.enabled = true;
            animator.SetBool("SinBalas", true);
            dispararPermitido = false;
            cambiarArma = true;
        }
    }
}
