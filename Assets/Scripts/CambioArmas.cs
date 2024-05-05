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
    public GameObject fpsCamera;
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
    }

    //Se encarga de activar y desactivar el arma seleccionada
    void ArmaSeleccionada()
    {
        int numArma = 0;

        foreach (Transform arma in transform) 
        {
            if (numArma ==  armaSeleccionada)
                arma.gameObject.SetActive(true);
            else
                arma.gameObject.SetActive(false);
            numArma++;
        }
    }

    //Se encarga de recibir el input Click izquierdo para disparar
    public void MiInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Disparo();
        }
    }

    //Se encarga de disparar el rayo para el disparo
    void Disparo() 
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, Mathf.Infinity))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
