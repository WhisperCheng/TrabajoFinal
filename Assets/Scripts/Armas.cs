using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public class Armas : MonoBehaviour
{
    public int daño;
    public float timeBetweenShooting;
    public float tiempoRecarga;
    public float timeBetweenShoot;
    public int capacidadCargador;
    public int balasPorTap;
    int balasRestante;
    int balasDisparadas;

    public bool allowButtonHold;

    public float rango;

    bool disparando;
    bool preparadoDisparar;
    bool recargando;

    public Camera fpsCamara;
    public Transform puntoAtaque;
    public LayerMask enemigo;

    PlayerInput PlayerInput;

    void Awake()
    {
        balasRestante = capacidadCargador;
        preparadoDisparar = true;
    }
    void Start()
    {
        PlayerInput = GetComponent<PlayerInput>();
    }

    public void MyInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && preparadoDisparar && disparando && !recargando && balasRestante > 0)
        {
            Debug.Log("eh disparado");
            Disparo();
        }
    }
    private void Disparo()
    {

        RaycastHit hit;

        if (Physics.Raycast(fpsCamara.transform.position,fpsCamara.transform.forward, out hit, rango, enemigo))
        {
            Debug.Log(hit.collider.name);

            if (hit.collider.CompareTag("Enemigo"))
            {
                //Scritp de quitar vida al enemigo.
            }
        }
    }

    public void Recargar (InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && balasRestante < capacidadCargador && !recargando) 
        {

        }
    }
}
