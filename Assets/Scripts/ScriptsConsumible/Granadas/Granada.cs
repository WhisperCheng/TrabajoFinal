using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granada : MonoBehaviour
{
    //Declaraciones
    Rigidbody rb;

    public GameObject granadaEfecto;
    public GameObject granadaEfectoBorrar;
    public bool boolCooldown;

    //Variable float que se encarga del tiempo de la granada para explotar
    public float temporizadorGranada;

    //Variable float para el tamaño del radio de explosion
    public float radioExplosion;

    //Variable int para el daño que hace la granada
    public int daño;

    //Variable int para la mascara del enemigo
    public int mascaraEnemigo;


    // Start is called before the first frame update
    void Start()
    {
        daño = 80;
        radioExplosion = 10;
        mascaraEnemigo = 1 << 8;
        temporizadorGranada = 3;
        rb = GetComponent<Rigidbody>();
        granadaEfecto = Resources.Load<GameObject>("SmallExplosion");
    }

    // Update is called once per frame
    void Update()
    {
        //Se encarga de restar el tiempo de la granada para explotar
        temporizadorGranada -= Time.deltaTime;

        //Se encarga de calcular la explosion
        explosion();
    }

   //Se encarga de la explosion y de hacer daño
    public void explosion()
    {
        if (temporizadorGranada <= 0 && boolCooldown == false)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.localPosition, radioExplosion, mascaraEnemigo);

            foreach (var hitcollider in hitColliders)
            {
                hitcollider.GetComponent<Enemigo>().hitDaño(daño);
                Debug.Log(hitcollider);
            }
            //Desactivar este destroy para poder probar el radio de las explosiones
            granadaEfectoBorrar = Instantiate(granadaEfecto, transform.localPosition, Quaternion.identity);
            Destroy(granadaEfectoBorrar, 1);
            Destroy(gameObject, 0.1f);
            boolCooldown = true;
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.localPosition, radioExplosion);
    }
}
