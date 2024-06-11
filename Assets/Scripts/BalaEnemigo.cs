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
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == 6 || collision.collider.gameObject.layer == 10)
        {
            destruir();
        }
        else if (collision.collider.tag == "Personaje")
        {
            GameManager.Instance.dañoRecibido();    
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
