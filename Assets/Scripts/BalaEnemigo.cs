using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    Rigidbody rb;
    public float velocidadBala;
    // Start is called before the first frame update
    void Start()
    {
        velocidadBala = 15;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * velocidadBala, ForceMode.Impulse);
        Invoke("destruir", 3); 
    }

    // Update is called once per frame
    void Update()
    {
 
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }
        else if (collision.collider.tag == "Personaje")
        {
            GameManager.Instance.da�oRecibido();
            Destroy(gameObject);
        }
    }
    public void destruir()
    {
        Destroy(gameObject);
    }
}
