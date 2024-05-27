using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float velocidadBase;
    public float fuerzaSalto;
    public float sensibilidad;
    public float reduccionDa�o;
    public float puntosVidaMaxima;
    public float puntosVidaActual;
    public float regeneracionPerSegundoBase;
    public float regeneracionPerSegundoActual;
    public float cooldownRegeneracion;
    public float velocidadActual;
    public int saltosExtrasBase;
    public int consumiblesBase;
    public int consumiblesRestantes;
    public int inyeccionesBase;
    public int inyeccionesRestantes;

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
            velocidadBase = 12;
            fuerzaSalto = 5;
            sensibilidad = 20;
            saltosExtrasBase = 1;
            reduccionDa�o = 0;
            puntosVidaMaxima = 100;
            regeneracionPerSegundoBase = 1;
            consumiblesBase = 3;
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
        cooldownRegeneracion += Time.deltaTime;

        regeneracionVida();
    }
    public void regeneracionVida()
    {
        if (cooldownRegeneracion >= 3)
        {
            puntosVidaActual += regeneracionPerSegundoActual * Time.deltaTime;

            if (puntosVidaActual > puntosVidaMaxima)
            {
                puntosVidaActual = puntosVidaMaxima;
            }
        }
    }

    public void AumentarVelocidad()
    {
        velocidadBase += 10;
    }
    public void AumentarFuerzaSalto()
    {
        fuerzaSalto += 10;
    }
    public void AumentarSaltosExtras()
    {
        saltosExtrasBase += 1;
    }
    public void AumentarReduccionDa�o()
    {
        reduccionDa�o += 10;
    }
    public void AumentarCapacidadConsumibles()
    {
        consumiblesBase += 1;
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
