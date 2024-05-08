using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public abstract class ArmasDatos : MonoBehaviour
{
    //Variables de datos generales que tendran las armas
    public int daño;
    public float cadencia;
    public float tiempoParaDisparar;

    //Variable necesaria que tendran todas las armas siempre con el mismo valor
    public float rango;

    //Variables int para la capacidad del cargador de las armas y el poder saber las balas para recargar
    public int cargador;
    public int balasRestantes;

    //Booleano simple para saber si el arma es automatica o no
    public bool armaAutomatica;

    //Declaraciones para la posicion de las armas al ser recogidas y la posicion de la camara para el disparo
    public GameObject fpsCamera;
    public GameObject armaHolster;

    //Variables en prueba para el sway del arma y el tilt para el movimiento
    public float intensidadRotacion;
    public float smoothRotacion;

    public Quaternion rotacionOrigen;

    //Hablar con el maestro para ver como hacer que detecte el script sin tener que asignarlo mediante interfaz de Unity
    public MovimientoCamara Vector2moveCamera;

    protected void Start()
    {
        fpsCamera = GameObject.Find("CamaraPrimeraPersona");
        armaHolster = GameObject.Find("Arma");
        rango = Mathf.Infinity;
        tiempoParaDisparar = 0;
    }

    public abstract void Disparar();

    //Nota: posibles datos necesarios para mas tarde

    /*public void morir()
    {
        Destroy(this.gameObject);
    }*/

    //abstract y override

}
