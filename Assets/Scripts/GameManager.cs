using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    public float velocidadBase;
    public float fuerzaSalto;
    public float sensibilidad;
    public float puntosVidaMaxima;
    public float puntosVidaActual;
    public float regeneracionPerSegundoBase;
    public float regeneracionPerSegundoActual;
    public float velocidadActual;
    public bool inyeccionActivada;
    public int saltosExtrasBase;
    public int consumiblesBase;
    public int consumiblesRestantes;
    public int inyeccionesBase;
    public int inyeccionesRestantes;
    public int reduccionDaño;
    public int puntos;
    public int enemigosEliminados;
    public int numeroOleada;
    public int consumiblesRecolectados;
    public int velocidadBaseMaxima;
    public int consumiblesBaseMaxima;
    public int reduccionDañoMaxima;
    public int fuerzaSaltoMaxima;
    public int saltosExtrasBaseMaxima;
    PostProcessVolume volumenPostProcess;
    Vignette vignette;

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
    public void regeneracionVida()
    {   
        puntosVidaActual += regeneracionPerSegundoActual * Time.deltaTime;

        if (puntosVidaActual > puntosVidaMaxima)
        {
            puntosVidaActual = puntosVidaMaxima;
        }
    }
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
    public void dañoRecibidoCooldown()
    {
        vignette.intensity.Override(0);
    }
    public void AumentarVelocidad()
    {
        if (velocidadBase != velocidadBaseMaxima)
        {
            velocidadBase += 4;
        }
    }
    public void AumentarFuerzaSalto()
    {
        if (fuerzaSalto != fuerzaSaltoMaxima)
        {
            fuerzaSalto += 2;
        }
    }
    public void AumentarSaltosExtras()
    {
        if (saltosExtrasBase != saltosExtrasBaseMaxima)
        {
            saltosExtrasBase += 1;
        }
    }
    public void AumentarReduccionDaño()
    {
        if (reduccionDaño != reduccionDañoMaxima)
        {
            reduccionDaño += 2;
        }
    }
    public void AumentarCapacidadConsumibles()
    {
        if (consumiblesBase != consumiblesBaseMaxima)
        {
            consumiblesBase += 1;
            consumiblesRestantes += 1;
        }

    }
    public void AumentarNumInyeccion()
    {
        if (inyeccionesRestantes < inyeccionesBase)
        {
            inyeccionesRestantes++;
        }
    }
    public void AumentarNumConsumibles()
    {
        if (consumiblesRestantes < consumiblesBase)
        {
            consumiblesRestantes++;
        }
    }
    public void puntosFinal()
    {
        puntos = enemigosEliminados * (numeroOleada + consumiblesRecolectados);
    }
}
