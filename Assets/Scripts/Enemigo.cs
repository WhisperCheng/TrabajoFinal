using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int vidaEnemigo;
    // Start is called before the first frame update
    void Start()
    {
        vidaEnemigo = 100;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void hitDa�o(int da�oArma)
    {
        vidaEnemigo -= da�oArma;
        Debug.Log("Enemigo: Eh recibido da�o");
        if (vidaEnemigo <= 0)
        {
            Destroy(gameObject);
        }
    }
}
