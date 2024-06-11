using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    //Variable float
    public float velocidadBala;

    //Variables GameObject
    public GameObject impactoBalaEnemigo;
    public GameObject impactoBalaEnemigoBorrar;

    //Declaraciones
    Rigidbody rb;
    public ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        velocidadBala = 15;
        rb = GetComponent<Rigidbody>();
        impactoBalaEnemigo = Resources.Load<GameObject>("TinyExplosion");
        rb.AddForce(transform.forward * velocidadBala, ForceMode.Impulse);
        Invoke("destruir", 3);
        particle.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Se encarga de ver con lo que colisiona
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6 || other.gameObject.layer == 10)
        {
            destruir();
        }
        else if (other.tag == "Personaje")
        {
            GameManager.Instance.daņoRecibido();    
            destruir();
        }        
    }

    //Se encarga de destruirse y ejecutar un efecto visual
    public void destruir()
    {
        impactoBalaEnemigoBorrar = Instantiate(impactoBalaEnemigo, transform.position, Quaternion.identity);
        Destroy(impactoBalaEnemigoBorrar, 1);
        Destroy(gameObject);
    }
}
