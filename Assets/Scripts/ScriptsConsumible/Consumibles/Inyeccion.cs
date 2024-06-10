using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inyeccion : MonoBehaviour
{
    //Declaracion
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    //Se encarga de activar la animacion y el objeto y de añadir un pequeño cooldown para compenetrar la animacion con el efecto
    public void EfectoInyeccion()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("Inyeccion");
        Invoke("EfectoInyeccionCooldown", 0.5f);
    }

    //Se encarga de aumantar los datos correspondiente de los efectos de la inyeccion y activar el cooldown del fin
    public void EfectoInyeccionCooldown()
    {
        Invoke("EfectoInyeccionFin", 3);
        GameManager.Instance.inyeccionesRestantes--;
        GameManager.Instance.inyeccionActivada = true;
        GameManager.Instance.velocidadActual += 15;
        GameManager.Instance.regeneracionPerSegundoActual += 5;
    }

    //Se encarga de quitar los buffs de la inyeccion y volver como antes
    public void EfectoInyeccionFin()
    {
        GameManager.Instance.velocidadActual = GameManager.Instance.velocidadBase;
        GameManager.Instance.regeneracionPerSegundoActual = GameManager.Instance.regeneracionPerSegundoBase;
        GameManager.Instance.inyeccionActivada = false;
    }
}
