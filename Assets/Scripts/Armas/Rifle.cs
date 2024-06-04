using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Rifle : ArmasDatos
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        daño = 10;
        cargador = 71;
        balasRestantes = cargador;
        armaAutomatica = true;
    }
    void Update()
    {
        //Se encarga del balanceo del arma
        balanceoArma();

        //Se encarga de comprobar las balas del arma y cambiar el estado en dicho caso
        Sinbalas();
    }

    //Se encarga de lanzar el rayo para dañar al enemigo
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
