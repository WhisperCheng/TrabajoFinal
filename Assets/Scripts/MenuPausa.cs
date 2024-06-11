using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuPausa : MonoBehaviour
{

    public static bool juegoPausado;

    public GameObject interfazMenu;

    // Start is called before the first frame update
    void Start()
    {
        juegoPausado = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void comprobarMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (juegoPausado == true)
            {
                Volver();
            }
            else
            {
                Pausar();
            }
        }
    }
    public void Volver()
    {
        Time.timeScale = 1f;
        juegoPausado = false;
        interfazMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Pausar()
    {
        Time.timeScale = 0f;
        juegoPausado = true;
        interfazMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}
