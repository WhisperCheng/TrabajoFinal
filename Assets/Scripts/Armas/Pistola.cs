using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pistola : ArmasDatos
{
    // sobrecarga de metodos utilizada
    new void Start()
    {
        // Llamo a la clase start del padre
        base.Start();
        daño = 10;
        cadencia = 1f;
        cargador = 30;
        balasRestantes = cargador;
        armaAutomatica = false;
        armaPorRecolectar = true;
    }
    void Update()
    {  
        balanceoArma();
        Sinbalas();
    }
    public override void Disparar()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, rango) && dispararPermitido == true) 
        {
            if (hit.collider.tag == "Enemigo")
            {
                hit.collider.gameObject.GetComponent<Enemigo>().hitDaño(daño);
            }
            Debug.Log(hit.transform.name);
        }
    }
}
