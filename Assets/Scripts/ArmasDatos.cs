using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class ArmasDatos : MonoBehaviour
{
    public int daño;
    public float rango;
    public float cadencia;
    public float tiempoParaDisparar;
    public int cargador;
    public int balasRestantes;
    public bool armaAutomatica;
    public GameObject fpsCamera;

    protected void Start()
    {
        fpsCamera = GameObject.Find("CamaraPrimeraPersona");
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
