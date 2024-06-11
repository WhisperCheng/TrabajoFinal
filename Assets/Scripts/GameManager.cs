using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    //Declaraciones
    PostProcessVolume volumenPostProcess;
    Vignette vignette;

    //Variables de datos base que se utilizan para guardar datos
    public float velocidadBase;
    public float puntosVidaMaxima;
    public float regeneracionPerSegundoBase;
    public int saltosExtrasBase;
    public int consumiblesBase;
    public int inyeccionesBase;

    //Variables de datos restante que son las que se utilizan y se modifican constantemente
    public float velocidadActual;
    public float puntosVidaActual;
    public float fuerzaSalto;    
    public int inyeccionesRestantes;
    public int consumiblesRestantes;
    public int reduccionDaño;

    //Variables de datos de la mayor cantidad posible de datos base
    public int velocidadBaseMaxima;
    public int consumiblesBaseMaxima;
    public int reduccionDañoMaxima;
    public int fuerzaSaltoMaxima;
    public int saltosExtrasBaseMaxima;

    //Variables float de distintos usos
    public float sensibilidad;
    public float regeneracionPerSegundoActual;

    //Variable bool para saber si la inyeccion esta activada
    public bool inyeccionActivada;

    //Variables para calcular los puntos ganados
    public int puntos;
    public int enemigosEliminados;
    public int numeroOleada;
    public int consumiblesRecolectados;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            volumenPostProcess = GameObject.Find("PostProcessing").GetComponent<PostProcessVolume>();
            volumenPostProcess.profile.TryGetSettings<Vignette>(out vignette);
            Instance = this;
            velocidadBase = 8;
            fuerzaSalto = 4;
            sensibilidad = 20;
            puntosVidaMaxima = 100;
            regeneracionPerSegundoBase = 1;
            consumiblesBase = 1;
            inyeccionesBase = 1;
            fuerzaSaltoMaxima = 8;
            velocidadBaseMaxima = 16;
            reduccionDañoMaxima = 8;
            saltosExtrasBaseMaxima = 3;
            consumiblesBaseMaxima = 5;
            numeroOleada = 1;
            puntosVidaActual = puntosVidaMaxima;
            inyeccionesRestantes = inyeccionesBase;
            velocidadActual = velocidadBase;
            regeneracionPerSegundoActual = regeneracionPerSegundoBase;
            consumiblesRestantes = consumiblesBase;
        }
    }

    // Update is called once per frame
    void Update()
    {
        regeneracionVidaTiempo();
    }

    //Se encarga de activar la regeneracion de vida despues de tres segundos o si utilizas la inyeccion
    public void regeneracionVidaTiempo()
    {
        if (puntosVidaActual < 100)
        {
            Invoke("regeneracionVida", 3);
        }
        if (inyeccionActivada == true)
        {
            regeneracionVida();
        }
    }

    //Se encarga de la regeneracion de vida del personaje
    public void regeneracionVida()
    {   
        puntosVidaActual += regeneracionPerSegundoActual * Time.deltaTime;

        if (puntosVidaActual > puntosVidaMaxima)
        {
            puntosVidaActual = puntosVidaMaxima;
        }
    }

    //Se encarga de actualizar el recibir daño del personaje
    public void dañoRecibido()
    {
        vignette.intensity.Override(0.4f);
        Invoke("dañoRecibidoCooldown", 0.3f);
        puntosVidaActual -= 10 - reduccionDaño;
        if (puntosVidaActual == 0)
        {
            //Tengo que poner que mueras y se ejecute algo como animacion para ejecutar el menu donde estan los datos
        }
    }

    //Se encarga de quitar el efecto visual de camara del personaje
    public void dañoRecibidoCooldown()
    {
        vignette.intensity.Override(0);
    }

    //Se encarga de aumentar la velocidad del personaje cuando recoges la habilidad especifica
    public void AumentarVelocidad()
    {
        if (velocidadBase != velocidadBaseMaxima)
        {
            velocidadBase += 4;
        }
    }

    //Se encarga de aumentar la altura de salto del personaje cuando recoges la habilidad especifica
    public void AumentarFuerzaSalto()
    {
        if (fuerzaSalto != fuerzaSaltoMaxima)
        {
            fuerzaSalto += 2;
        }
    }

    //Se encarga de aumentar la cantidad de saltos extras
    public void AumentarSaltosExtras()
    {
        if (saltosExtrasBase != saltosExtrasBaseMaxima)
        {
            saltosExtrasBase += 1;
        }
    }

    //Se encarga de aumentar la reduccion de daño dependiendo de la habilidad recogida
    public void AumentarReduccionDaño()
    {
        if (reduccionDaño != reduccionDañoMaxima)
        {
            reduccionDaño += 2;
        }
    }

    //Se encarga de aumentar la capacidad de granadas que tienes cuando recoges la habilidad especifca
    public void AumentarCapacidadConsumibles()
    {
        if (consumiblesBase != consumiblesBaseMaxima)
        {
            consumiblesBase += 1;
            consumiblesRestantes += 1;
        }

    }

    //Se encarga de aumentar el numero de inyecciones
    public void AumentarNumInyeccion()
    {
        if (inyeccionesRestantes < inyeccionesBase)
        {
            inyeccionesRestantes++;
        }
    }

    //Se encarga de aumentar el numero maximo de consumibles
    public void AumentarNumConsumibles()
    {
        if (consumiblesRestantes < consumiblesBase)
        {
            consumiblesRestantes++;
        }
    }

    //Se encarga de calcular los puntos finales
    public void puntosFinal()
    {
        puntos = enemigosEliminados * (numeroOleada + consumiblesRecolectados);
    }
}
