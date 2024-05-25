using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Consumible : MonoBehaviour
{
    //Tengo que testestear esto en el ordenador con un objeto de test, IMPORTANTE: debo testear TODO
    //Tambien debo de optimizar los scripts para los distintos objetos tanto consumibles(Inyeccion) y los lanzables

    public int consumiblesRestantes;
    public float fuerzaLanzamiento;
    public float contadorGranada;
    public float radioExplosion;
    public int mascaraEnemigo;
    public GameObject tipoGranada;
    public ArmasDatos holsterArmasDatos;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        consumiblesRestantes = GameManager.Instance.consumiblesBase;
        rb = GetComponent<Rigidbody>();
        fuerzaLanzamiento = 5;
        radioExplosion = 4;
        mascaraEnemigo = 1 << 7;
    }

    // Update is called once per frame
    void Update()
    {
        contadorGranada += Time.deltaTime;
        explosion();
    }
    public void lanzarGranada(InputAction.CallbackContext context)
    {
        if (context.performed && consumiblesRestantes > 0)
        {
            contadorGranada = 0;
            Instantiate(tipoGranada, holsterArmasDatos.GetComponent<ArmasDatos>().armaHolster.transform.localPosition, Quaternion.identity);
            rb.AddForce(Vector3.forward * fuerzaLanzamiento, ForceMode.Impulse);
        }
    }
    public void explosion()
    {
        //Mirar esto del "contadorGranada" con bastante ojo ya que va a dar problemas de creacion constantemente
        if (contadorGranada > 3) 
        {
            Collider[] hitColliders = Physics.OverlapSphere(tipoGranada.transform.localPosition, radioExplosion, mascaraEnemigo);

            foreach (var hitcollider in hitColliders)
            {
                hitcollider.GetComponent<Collider>().attachedRigidbody.AddExplosionForce(10, tipoGranada.transform.localPosition, radioExplosion, 77, ForceMode.Impulse);
            }
        }
        contadorGranada = 0;
    }
}
