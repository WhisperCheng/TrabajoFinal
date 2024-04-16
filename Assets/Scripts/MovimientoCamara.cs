using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoCamara : MonoBehaviour
{

    PlayerInput playerInput;

    float sensibilidad;
    float rotacionVertical;
    public Transform cuerpoPersonaje;
    // Start is called before the first frame update
    void Start()
    {
        sensibilidad = 10f;
        playerInput = GetComponent<PlayerInput>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputMovimientoCamara = playerInput.actions["Look"].ReadValue<Vector2>() * sensibilidad * Time.deltaTime;

        rotacionVertical -= inputMovimientoCamara.y;
        rotacionVertical = Mathf.Clamp(rotacionVertical, -90, 90);

        transform.localRotation = Quaternion.Euler(rotacionVertical, 0, 0);
        cuerpoPersonaje.Rotate(Vector3.up * inputMovimientoCamara.x);
    }
}
