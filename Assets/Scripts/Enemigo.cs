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
    public void hitDaño(int dañoArma)
    {
        vidaEnemigo -= dañoArma;
        Debug.Log("Enemigo: Eh recibido daño");
        if (vidaEnemigo <= 0)
        {
            Destroy(gameObject);
        }
    }
}
