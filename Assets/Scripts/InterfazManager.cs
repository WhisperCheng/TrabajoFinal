using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfazManager : MonoBehaviour
{
    public TMP_Text textVidas;
    public TMP_Text textBalas;
    public TMP_Text textGranadas;
    public TMP_Text textMensaje;
    public TMP_Text textInyeccion;
    public TMP_Text textInformacion;
    public TMP_Text[] textPantallas;
    public CambioArmas armas;
    public SpawnManager spawnManager;
    public Movimiento movimiento;

    public static InterfazManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            textMensaje.gameObject.SetActive(false);
            textInformacion.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        interfazVidas();
        interfazBalas();
        interfazGranadas();
        interfazInyeccion();
    }
    public void interfazVidas()
    {
        if (GameManager.Instance.puntosVidaActual < 35)
        {
            textVidas.color = Color.red;
            textVidas.text = GameManager.Instance.puntosVidaActual.ToString("F0") + " / " + GameManager.Instance.puntosVidaMaxima;
        }
        else
        {
            textVidas.color = Color.black;
            textVidas.text = GameManager.Instance.puntosVidaActual.ToString("F0") + " / " + GameManager.Instance.puntosVidaMaxima;
        }
    }
    public void interfazBalas()
    {
        if (armas.armaActiva.GetComponent<ArmasDatos>().balasRestantes < (armas.armaActiva.GetComponent<ArmasDatos>().cargador / 2))
        {
            textBalas.color = Color.red;
            textBalas.text = armas.armaActiva.GetComponent<ArmasDatos>().balasRestantes + " / " + armas.armaActiva.GetComponent<ArmasDatos>().cargador;
        }
        else
        {
            textBalas.color= Color.black;
            textBalas.text = armas.armaActiva.GetComponent<ArmasDatos>().balasRestantes + " / " + armas.armaActiva.GetComponent<ArmasDatos>().cargador;
        }
    }
    public void interfazGranadas()
    {
        textGranadas.text = GameManager.Instance.consumiblesRestantes + " / " + GameManager.Instance.consumiblesBase;
    }
    public void interfazInyeccion()
    {
        textInyeccion.text = GameManager.Instance.inyeccionesRestantes + " / " + GameManager.Instance.inyeccionesBase;
    }
    public void interfazMensaje()
    {
        textMensaje.gameObject.SetActive(true);
        textMensaje.text = "Ronda Finalizada";
        Invoke("interfazMensajeComienzo", 8);
    }
    public void interfazMensajeComienzo()
    {
        textMensaje.text = "Comenzando Ronda";
    }
    public void interfazInformacion()
    {
        textInformacion.gameObject.SetActive(true);
        textInformacion.text = movimiento.nombreObjeto + " Recolectado";
        Invoke("interfazInformacionDesactivar", 1);
    }
    public void interfazInformacionDesactivar()
    {
        textInformacion.gameObject.SetActive(false);
    }
    public void interfazPantallas()
    {
        foreach (TMP_Text pantalla in textPantallas)
        {
            pantalla.text = spawnManager.numOleada + "ª Oleada\n" + spawnManager.enemigosRestantes + " Enemigos Restantes";
        }
    }
}
