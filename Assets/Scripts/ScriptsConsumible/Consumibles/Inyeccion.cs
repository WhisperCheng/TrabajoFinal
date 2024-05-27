using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inyeccion : MonoBehaviour, IRecogerConsumible
{
    //Tengo que testear esto en clase con el bind de teclado y mando hecho y ver que funciona y se escala de forma correcta
    //Posiblemente sea necesario agregarle un collider y desactivarlo al utilizar la habilidad por posibles bugs

    bool efectoInyeccion;
    float cooldownEfectoVelocidad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldownEfectoVelocidad += Time.deltaTime;

        efectoVelocidad();
    }
    public void usarInyeccion(InputAction.CallbackContext context)
    {
        if (context.performed && GameManager.Instance.inyeccionesRestantes == GameManager.Instance.inyeccionesBase)
        {
            GameManager.Instance.inyeccionesRestantes--;
            GameManager.Instance.cooldownRegeneracion = 5;
            efectoInyeccion = true;
            cooldownEfectoVelocidad = 0;
        }
    }
    public void efectoVelocidad()
    {
        if (efectoInyeccion == true)
        {
            if (cooldownEfectoVelocidad < 3)
            {
                GameManager.Instance.velocidadActual += 10;
                GameManager.Instance.regeneracionPerSegundoActual += 5;
            }
        }
        else if (cooldownEfectoVelocidad > 3)
        {
            GameManager.Instance.velocidadActual = GameManager.Instance.velocidadBase;
            GameManager.Instance.regeneracionPerSegundoActual = GameManager.Instance.regeneracionPerSegundoBase;
            efectoInyeccion = false;
        }
    }

    public void consumibleRecolectado()
    {
        //Por ahora esto deberia funcionar falta testear la cosa es que no me gusta como se hace por lo que se mejorara en el futuro
        GameManager.Instance.AumentarNumInyeccion();
        Destroy(gameObject);
    }
}
