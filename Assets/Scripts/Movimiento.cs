using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento : MonoBehaviour
{
    //declaraciones
    PlayerInput playerInput;
    CharacterController characterController;

    //variables
    public float velocidadMovimiento;
    bool colisionSuelo;
    float cooldownSalto;
    public int saltosExtras;
    public int saltosExtrasRestantes;
    float longitudRayo;
    float fuerzaGravedad;
    float fuerzaSalto;
    float cooldownSlide;
    Vector3 vectorSalto;

    // Start is called before the first frame update
    void Start()
    {
        fuerzaGravedad = -9.81f;
        fuerzaSalto = 5;
        longitudRayo = 1.1f;
        saltosExtras = 1;
        velocidadMovimiento = 12;
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownSlide += Time.deltaTime;
        cooldownSalto += Time.deltaTime;
    }
    private void FixedUpdate()
    {
        //Movimiento del personaje
        Vector2 inputMovimiento = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 move = transform.right * inputMovimiento.x + transform.forward * inputMovimiento.y;
        characterController.Move(move * velocidadMovimiento * Time.deltaTime);

        //Se encarga de añadir gravedad al personaje
        vectorSalto.y += fuerzaGravedad * Time.deltaTime;
        characterController.Move(vectorSalto * Time.deltaTime);

        //Se encarga de mirar si el personaje esta tocando "algo" para poder saltar
        rayoSalto();
    }

    //Se encarga de el salto del personaje
    public void saltar(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && colisionSuelo == true)
        {
            vectorSalto.y = fuerzaSalto;
            cooldownSalto = 0;
            colisionSuelo = false;
        }
        if (context.phase == InputActionPhase.Performed && cooldownSalto >= 0.1f && saltosExtrasRestantes > 0)
        {
            vectorSalto.y = fuerzaSalto;
            cooldownSalto = 0;
            saltosExtrasRestantes -= 1;
        }
    }
    // se encarga de hacer el slide durante X segundos
    public void slide(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && colisionSuelo == true && cooldownSlide >= 2f)
        {
            velocidadMovimiento = 24;
            transform.localScale = new Vector3 (transform.localScale.x, 0.5f, transform.localScale.z);
            cooldownSlide = 0;
        }
        else
        {
            velocidadMovimiento = 12;
            transform.localScale = new Vector3 (transform.localScale.x, 1, transform.localScale.z);
        }
    }

    //Se encarga de mirar si se esta tocando "algo" para poder saltar
    public void rayoSalto()
    {
        Vector3 origen = transform.position;

        Vector3 direccion = -transform.up;

        RaycastHit golpeSuelo;

        if (Physics.Raycast(origen, direccion, out golpeSuelo, longitudRayo)) 
        {
            colisionSuelo = true;
            saltosExtrasRestantes = saltosExtras;
            vectorSalto.y = -2f;
        }
        else
        {
            colisionSuelo = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 direccionGizmo = -transform.up * 1.1f;
        Gizmos.DrawRay(transform.position, direccionGizmo);
    }
}
