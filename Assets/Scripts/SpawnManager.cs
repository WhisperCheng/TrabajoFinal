using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawn;
    public GameObject enemigo;
    public int enemigosRestantes;
    Transform spawnElegido;
    public int spawnCountRestante;
    public int spawnCountBase;
    public int numOleada;
    public float cooldownSpawn;
    public bool cooldown;
    public bool boolCambioOleada;


    //El spawn funciona correctamente el problema es que no se puede poner en Update ya que rompe todo
    //Por lo que debo utilizar las variables de la interfaz de cuando tienes tiempo para comprar para hacer el loop
    //Tambien puedo preguntar al maestro para ver si hay alguna forma buenas para hacerlo


    // Start is called before the first frame update
    void Start()
    {
        spawnCountBase = 2;
        spawnCountRestante = spawnCountBase;
        enemigosRestantes = spawnCountBase;
        numOleada = 1;
        cooldownSpawn = 3;
        spawnCantidad();
        InterfazManager.Instance.interfazPantallas();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void cambioOleada()
    {
        InterfazManager.Instance.textMensaje.gameObject.SetActive(false);
        numOleada++;
        GameManager.Instance.numeroOleada++;
        spawnCountBase += 2;
        spawnCountRestante = spawnCountBase;
        enemigosRestantes = spawnCountBase;
        spawnCantidad();
        InterfazManager.Instance.interfazPantallas();
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
            boolCambioOleada = false;
            Invoke("spawnEnemigo", cooldownSpawn);
        }
    }

    public void comprobarNumeroEnemigos()
    {
        if (spawnCountRestante == 0 && enemigosRestantes == 0)
        {
            boolCambioOleada = true;
            InterfazManager.Instance.interfazMensaje();
            Invoke("cambioOleada", 10);
        }
    }
    public void coolDown()
    {
        cooldown = false;
        spawnCantidad();
    }
    public void spawnEnemigo()
    {
        if (cooldown == false)
        {
            spawnElegido = spawn[Random.Range(0, 8)];
            Instantiate(enemigo, spawnElegido.transform.position, Quaternion.identity);
            Invoke("coolDown", 0.1f);
            cooldown = true;
            spawnCountRestante--;
            InterfazManager.Instance.interfazPantallas();
        }
    }
}
