using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawn;
    public GameObject enemigo;
    Transform spawnElegido;
    public int spawnCountRestante;
    public int spawnCountBase;
    public int numOleada;
    public float cooldownSpawn;
    public bool cooldown;


    //El spawn funciona correctamente el problema es que no se puede poner en Update ya que rompe todo
    //Por lo que debo utilizar las variables de la interfaz de cuando tienes tiempo para comprar para hacer el loop
    //Tambien puedo preguntar al maestro para ver si hay alguna forma buenas para hacerlo


    // Start is called before the first frame update
    void Start()
    {
        spawnCountBase = 2;
        spawnCountRestante = spawnCountBase;
        numOleada = 1;
        cooldownSpawn = 3;
        //spawnCantidad();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void cambioOleada()
    {
        numOleada++;
        spawnCountBase += 2;
        spawnCountRestante = spawnCountBase;
        if (numOleada == 10)
        {
            cooldownSpawn = 1.5f;
        }
    }
    public void spawnCantidad()
    {
        Debug.Log("Spawneando enemigos");
        if (spawnCountRestante > 0)
        {
            Invoke("spawnEnemigo", cooldownSpawn);
        }
        else if (spawnCountRestante == 0)
        {
            Invoke("cambioOleada", 20);
        }
    }
    public void coolDown()
    {
        cooldown = false;
        //spawnCantidad();
    }
    public void spawnEnemigo()
    {
        if (cooldown == false)
        {

            spawnElegido = spawn[Random.Range(0, 7)];
            Instantiate(enemigo, spawnElegido.transform.position, Quaternion.identity);
            Invoke("coolDown", 0.1f);
            cooldown = true;
            spawnCountRestante--;
        }
    }
}
