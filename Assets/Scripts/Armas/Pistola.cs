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
        da�o = 10;
        cargador = 30;
        balasRestantes = cargador;
        armaAutomatica = false;
    }
    void Update()
    {  
        //Se encarga del balanceo del arma
        balanceoArma();

        //Se encarga de comprobar las balas del arma y cambiar el estado en dicho caso
        Sinbalas();
    }

    //Se encarga de lanzar el rayo para da�ar al enemigo
    public override void Disparar()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, rango) && dispararPermitido == true) 
        {
            if (hit.collider.tag == "Enemigo")
            {
                hit.collider.gameObject.GetComponent<Enemigo>().hitDa�o(da�o);
            }
            Debug.Log(hit.transform.name);
        }
    }
}
