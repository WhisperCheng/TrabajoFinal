using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenasManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void botonJugar()
    {
        escenaJuego();
    }
    public void escenaJuego()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Juego");
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void botonMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MenuPrincipal");
    }
}
