using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalculoDatosFinal : MonoBehaviour
{
    public TMP_Text datos;
    // Start is called before the first frame update
    void Start()
    {
        mostrarDatos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void mostrarDatos()
    {
        datos.text = "Enemigos eliminado = " + GameManager.Instance.enemigosEliminados + "\n"
            + "Rondas sobrevividas = " + GameManager.Instance.numeroOleada + "\n"
            + "Objetos recolectados = " + GameManager.Instance.consumiblesRecolectados + "\n"
            + "Puntos final = " + GameManager.Instance.puntos;
    }
}
