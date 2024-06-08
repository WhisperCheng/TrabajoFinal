using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    Rigidbody rb;
    public float velocidadBala;
    public ParticleSystem particle;
    public GameObject impactoBalaEnemigo;
    public GameObject impactoBalaEnemigoBorrar;
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
    public void destruir()
    {
        impactoBalaEnemigoBorrar = Instantiate(impactoBalaEnemigo, transform.position, Quaternion.identity);
        Destroy(impactoBalaEnemigoBorrar, 1);
        Destroy(gameObject);
    }
}
