using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoCamara : MonoBehaviour
{
    //Declaraciones
    PlayerInput playerInput;

    //Variables float
    float rotacionVertical;

    //Variables transform para la rotacion del personaje
    public Transform cuerpoPersonaje;

    //Declaracion del Vector2 para el movimiento de la camara
    public Vector2 inputMovimientoCamara;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        //Sirve para hacer desaperecer el raton
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Todo el movimiento de la camara en primera persona
        inputMovimientoCamara = playerInput.actions["Look"].ReadValue<Vector2>() * GameManager.Instance.sensibilidad * Time.deltaTime;

        rotacionVertical -= inputMovimientoCamara.y;
        rotacionVertical = Mathf.Clamp(rotacionVertical, -90, 90);

        transform.localRotation = Quaternion.Euler(rotacionVertical, 0, 0);
        cuerpoPersonaje.Rotate(Vector3.up * inputMovimientoCamara.x);
    }
}
