using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granada : MonoBehaviour
{
    public float temporizadorGranada;
    public float radioExplosion;
    public int da�o;
    public int mascaraEnemigo;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        da�o = 80;
        radioExplosion = 10;
        mascaraEnemigo = 1 << 8;
        temporizadorGranada = 3;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        temporizadorGranada -= Time.deltaTime;
        explosion();
    }
    public void explosion()
    {
        if (temporizadorGranada <= 0)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.localPosition, radioExplosion, mascaraEnemigo);

            foreach (var hitcollider in hitColliders)
            {
                hitcollider.GetComponent<Enemigo>().hitDa�o(da�o);
                Debug.Log(hitcollider);
            }
            //Desactivar este destroy para poder probar el radio de las explosiones
            Destroy(gameObject);
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.localPosition, radioExplosion);
    }
}
