using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class CambioArmas : MonoBehaviour
{

    public int armaSeleccionada;
    public float cambioArmaRueda;
    GameObject armaActiva;
    PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        //Se encarga que al empezar el juego se te ponga la pistola
        ArmaSeleccionada();
    }

    // Update is called once per frame
    void Update()
    {
        //Se encarga de recibir el input de la rueda del raton en positivo o negativo
        cambioArmaRueda = playerInput.actions["CambiarArma"].ReadValue<float>();

        //Para comprobar las armas
        int previaArma = armaSeleccionada;

        //Se encarga de cambiar al arma que corresponde depenediendo del valor que tenga el float
        if (cambioArmaRueda > 0f)
        {
            if (armaSeleccionada >= transform.childCount - 1)
            {
                armaSeleccionada = 0;
            }
            else
                armaSeleccionada++;
        }
        if (cambioArmaRueda < 0f)
        {
            if (armaSeleccionada <= 0f)
            {
                armaSeleccionada = transform.childCount - 1;
            }
            else
                armaSeleccionada--;
        }

        //Cambia al arma que corresponde
        if (previaArma != armaSeleccionada)
        {
            ArmaSeleccionada();
        }

        //Se encarga de que si el arma es automatica se mantenga el fuego constante
        //Depenediendo del arma activa tendra mayor cadencia o no
        if (playerInput.actions["Disparando"].IsPressed() && armaActiva.GetComponent<ArmasDatos>().armaAutomatica == true)
        {
            if (Time.time >= armaActiva.GetComponent<ArmasDatos>().tiempoParaDisparar)
            {
                if (armaActiva.GetComponent<ArmasDatos>().balasRestantes > 0) 
                { 
                    armaActiva.GetComponent<ArmasDatos>().tiempoParaDisparar = Time.time + 1 / armaActiva.GetComponent<ArmasDatos>().cadencia;
                    Debug.Log("Se a mantenido");
                    armaActiva.GetComponent<ArmasDatos>().Disparar();
                    armaActiva.GetComponent<ArmasDatos>().balasRestantes--;
                }
            }
        }
    }

    //Se encarga de activar y desactivar el arma seleccionada
    void ArmaSeleccionada()
    {
        int numArma = 0;

        foreach (Transform arma in transform) 
        {
            if (numArma == armaSeleccionada)
            {
                arma.gameObject.SetActive(true);
                armaActiva = arma.gameObject;
            }

            else
            {
                arma.gameObject.SetActive(false);
            }
            numArma++;
        }
    }
    public void MiInput(InputAction.CallbackContext context)
    {
        // Si el arma es semiAutomatica se ejecuta este apartado.
        if (context.started && armaActiva.GetComponent<ArmasDatos>().armaAutomatica == false)
        {
            armaActiva.GetComponent<ArmasDatos>().Disparar();
        }
    }
}
