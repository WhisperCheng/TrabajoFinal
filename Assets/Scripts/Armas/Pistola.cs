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
        rotacionOrigen = transform.localRotation;

        //Mirar estos valores para hacer el balanceo mejor
        intensidadRotacion = 1;
        smoothRotacion = 10;
    }
    void Update()
    {
        balanceo();
    }
    public override void Disparar()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, Mathf.Infinity)) 
        {
            Debug.Log(hit.transform.name);
        }
    }
    public void balanceo()
    {
        Quaternion t_adj_x = Quaternion.AngleAxis(intensidadRotacion * Vector2moveCamera.inputMovimientoCamara.x, Vector3.up);
        Quaternion t_adj_y = Quaternion.AngleAxis(intensidadRotacion * Vector2moveCamera.inputMovimientoCamara.y, Vector3.right);
        Quaternion target_rotation = rotacionOrigen * t_adj_x * t_adj_y;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, target_rotation, Time.deltaTime * smoothRotacion);
    }
}
