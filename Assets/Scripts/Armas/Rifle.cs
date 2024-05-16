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
        daño = 20;
        cadencia = 15;
        cargador = 50;
        balasRestantes = cargador;
        armaAutomatica = true;

        //Ajustar los valores para el balanceo del arma
        intensidadRotacion = 10;
        smoothRotacion = 10;
    }
    void Update()
    {
        balanceoArma();
    }

    public override void Disparar()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, rango))
        {
            if (hit.collider.tag == "Enemigo")
            {
                hit.collider.gameObject.GetComponent<Enemigo>().hitDaño(daño);
            }
            Debug.Log(hit.transform.name);
        }
    }
}
