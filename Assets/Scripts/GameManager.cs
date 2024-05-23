using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float velocidadBase;
    public float fuerzaSalto;
    public float sensibilidad;
    public float reduccionDaño;
    public float puntosVida;
    public int saltosExtrasBase;
    public int consumiblesBase;

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
            reduccionDaño = 0;
            puntosVida = 100;
            consumiblesBase = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public void AumentarReduccionDaño()
    {
        reduccionDaño += 10;
    }
    public void AumentarCapacidadConsumibles()
    {
        consumiblesBase += 1;
    }
}
