using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfazManager : MonoBehaviour
{
    //Variables text
    public TMP_Text textVidas;
    public TMP_Text textBalas;
    public TMP_Text textGranadas;
    public TMP_Text textMensaje;
    public TMP_Text textInyeccion;
    public TMP_Text textInformacion;
    public TMP_Text textFinal;

    //Variable text con lista
    public TMP_Text[] textPantallas;

    //Declaraciones
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

    //Se encarga de modificar y enseñar por pantalla la cantidad de vida que tienes
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

    //Se encarga de modificar y calcular la cantidad de balas que tiene tu arma y enseñarla en pantalla
    public void interfazBalas()
    {
        if (armas.armaActiva.GetComponent<ArmasDatos>().balasRestantes < (armas.armaActiva.GetComponent<ArmasDatos>().cargador / 2))
        {
            textBalas.color = Color.red;
            textBalas.text = armas.armaActiva.GetComponent<ArmasDatos>().balasRestantes + " / " + armas.armaActiva.GetComponent<ArmasDatos>().cargador;
        }
        else
        {
            textBalas.color = Color.black;
            textBalas.text = armas.armaActiva.GetComponent<ArmasDatos>().balasRestantes + " / " + armas.armaActiva.GetComponent<ArmasDatos>().cargador;
        }
    }

    //Se encarga de enseñar la cantidad de granadas que tienes por pantalla
    public void interfazGranadas()
    {
        textGranadas.text = GameManager.Instance.consumiblesRestantes + " / " + GameManager.Instance.consumiblesBase;
    }

    //Se encarga de enseñar la cantidad de inyecciones que tienes por pantalla
    public void interfazInyeccion()
    {
        textInyeccion.text = GameManager.Instance.inyeccionesRestantes + " / " + GameManager.Instance.inyeccionesBase;
    }

    //Se encarga de enseñar por pantalla cuando va a comenzar la siguiente ronda y cuando acaba una
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

    //Se encarga de mostrarte por pantalla de si has recogido un objeto y el nombre de dicho objeto
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

    //Se encarga de enseñar por las pantallas de la escena la oleada en la que estas y los enemigos restantes
    public void interfazPantallas()
    {
        foreach (TMP_Text pantalla in textPantallas)
        {
            pantalla.text = spawnManager.numOleada + "ª Oleada\n" + spawnManager.enemigosRestantes + " Enemigos Restantes";
        }
    }
    public void final()
    {
        textFinal.text = "Enemigos eliminado = " + GameManager.Instance.enemigosEliminados + "\n" +
            "Rondas sobrevividas = " + GameManager.Instance.numeroOleada + "\n" +
            "Objetos recolectados = " + GameManager.Instance.consumiblesRecolectados +
            "Puntos final = " + GameManager.Instance.puntos;
    }
}
