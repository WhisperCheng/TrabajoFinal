using System.Collections;
using System.Collections.Generic;
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
    }
    public override void Disparar()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, Mathf.Infinity))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
