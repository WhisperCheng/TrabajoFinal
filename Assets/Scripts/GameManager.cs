using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            velocidadBase = 8;
            fuerzaSalto = 4;
            sensibilidad = 20;
            saltosExtrasBase = 0;
            reduccionDaño = 0;
            puntosVidaMaxima = 100;
            regeneracionPerSegundoBase = 1;
            consumiblesBase = 1;
            inyeccionesBase = 1;
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
        puntosVidaActual -= 10 - reduccionDaño;
        if (puntosVidaActual == 0)
        {
            //Tengo que poner que mueras y se ejecute algo como animacion para ejecutar el menu donde estan los datos
        }
    }
    public void AumentarVelocidad()
    {
        velocidadBase += 4;
    }
    public void AumentarFuerzaSalto()
    {
        fuerzaSalto += 2;
    }
    public void AumentarSaltosExtras()
    {
        saltosExtrasBase += 1;
    }
    public void AumentarReduccionDaño()
    {
        reduccionDaño += 2;
    }
    public void AumentarCapacidadConsumibles()
    {
        consumiblesBase += 1;
        consumiblesRestantes += 1;
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
}
