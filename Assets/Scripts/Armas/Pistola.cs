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
        cadencia = 1f;
        cargador = 30;
        balasRestantes = cargador;
        armaAutomatica = false;

        //Ajustar los valores para mejorar el balanceo
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
            Debug.Log(hit.transform.name);
        }
    }
}
