using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public abstract class ArmasDatos : MonoBehaviour, IRecogerArmas
{
    //Variables de datos generales que tendran las armas
    public int daño;
    public float cadencia;
    public float tiempoParaDisparar;

    //Bool utilizado para arreglar un pequeño bug que habia con el swing de las armas NO recolectadas
    public bool armaPorRecolectar;

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

    //Variable quaternion para el origen de la rotacion
    public Quaternion rotacionOrigen;

    //Declaracion del script para conseguir el movimiento del raton
    public MovimientoCamara Vector2moveCamera;

    //Sistema de particulas para hacer el flash del cañon del arma, faltaria ponerle un particula buena
    //Se debe de asignar el flash del arma desde la interfaz ya que no se como hacerlo mediante codigo
    public ParticleSystem muzzleFlash;

    protected void Start()
    {
        fpsCamera = GameObject.Find("CamaraPrimeraPersona");
        armaHolster = GameObject.Find("Arma");
        Vector2moveCamera = GameObject.Find("CamaraPrimeraPersona").GetComponent<MovimientoCamara>();
        intensidadRotacion = 5;
        smoothRotacion = 5;
        rango = Mathf.Infinity;
        rotacionOrigen = transform.localRotation;
        tiempoParaDisparar = 0;
    }

    //Se encarga del disparo del arma
    public abstract void Disparar();

    //Se encarga del balanceo del arma
    public void balanceoArma()
    {
        if (armaPorRecolectar == true)
        {
        Quaternion t_adj_x = Quaternion.AngleAxis(intensidadRotacion * Vector2moveCamera.inputMovimientoCamara.x, Vector3.up);
        Quaternion t_adj_y = Quaternion.AngleAxis(intensidadRotacion * Vector2moveCamera.inputMovimientoCamara.y, Vector3.right);
        Quaternion target_rotation = rotacionOrigen * t_adj_x * t_adj_y;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, target_rotation, Time.deltaTime * smoothRotacion);
        }
    }
    //Se encarga del recoger del arma y ponerlo en el ArmaHolster
    public void armaRecolectada()
    {
        if (armaPorRecolectar == false)
        {
            armaPorRecolectar = true;
            gameObject.SetActive(false);
            Instantiate(transform, armaHolster.transform.position, armaHolster.transform.rotation, armaHolster.transform);
            Destroy(gameObject);
        }
    }
}
