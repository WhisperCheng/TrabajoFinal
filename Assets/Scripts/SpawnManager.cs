using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Variables float
    public float cooldownSpawn;

    //Variables int
    public int enemigosRestantes;
    public int spawnCountRestante;
    public int spawnCountBase;
    public int numOleada;

    //Variables GameObject y transform
    public GameObject enemigo;
    Transform spawnElegido;

    //Listas de gameobject y transform
    public Transform[] spawn;
    public GameObject[] puertas;

    //Variables bool
    public bool cooldown;
    public bool boolCambioOleada;
    public bool activarPuertas;

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

    //Se encarga del cambio de oleadas
    void cambioOleada()
    {
        activarPuertas = false;
        InterfazManager.Instance.textMensaje.gameObject.SetActive(false);
        numOleada++;
        GameManager.Instance.numeroOleada++;
        spawnCountBase += 2;
        spawnCountRestante = spawnCountBase;
        enemigosRestantes = spawnCountBase;
        spawnCantidad();
        activacionPuertas();
        InterfazManager.Instance.interfazPantallas();
        if (numOleada == 10)
        {
            cooldownSpawn = 1.5f;
        }
    }

    //Se encarga de ver la cantidad de enemigos que se tiene que spawnear
    public void spawnCantidad()
    {
        Debug.Log("Spawneando enemigos");
        if (spawnCountRestante > 0)
        {
            boolCambioOleada = false;
            Invoke("spawnEnemigo", cooldownSpawn);
        }
    }

    //Se encarga de comprobar la cantidad de enemigos que hay en el nivel para poder pasar al void de "cambioOleada"
    public void comprobarNumeroEnemigos()
    {
        if (spawnCountRestante == 0 && enemigosRestantes == 0)
        {
            boolCambioOleada = true;
            activarPuertas = true;
            activacionPuertas();
            InterfazManager.Instance.interfazMensaje();
            Invoke("cambioOleada", 10);
        }
    }

    //Se encarga del coolDown del spawn de los enemigos
    public void coolDown()
    {
        cooldown = false;
        spawnCantidad();
    }

    //Se encarga de spawnear los enemigos
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

    //Se encarga de activar o desactivar las puertas dependiendo de la situacion
    public void activacionPuertas()
    {
        foreach (GameObject puerta in puertas)
        {
            if (activarPuertas == true)
            {
                puerta.gameObject.SetActive(true);
            }
            else
            {
                puerta.gameObject.SetActive(false);
            }
        }
    }
}
