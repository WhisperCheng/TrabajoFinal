using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Armas : MonoBehaviour
{
    //Estadisticas arma
    float daño;
    float rango;

    public GameObject fpsCamera;
    PlayerInput playerInput;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rango = 100;
    }
    void Update()
    {
        
    }

    public void MyInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Disparo();
        }
    }
    public void Disparo()
    {
            RaycastHit hit;

            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, rango))
            {
                Debug.Log(hit.transform.name);
            }
    }
}
