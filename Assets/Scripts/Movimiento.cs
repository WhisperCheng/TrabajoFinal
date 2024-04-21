using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Movimiento : MonoBehaviour
{
    //Declaraciones
    PlayerInput playerInput;
    CharacterController characterController;

    //Variables float "Cooldowns"
    public float cooldownSalto;
    public float cooldownSlide;
    public float cooldownDash;

    //Variables float "Velocidad"
    public float velocidad;
    float velocidadBase;
    public float inercia;

    //Variables float "Salto"
    float longitudRayo;
    float fuerzaGravedad;
    float fuerzaSalto;

    //Variables bool
    bool colisionSuelo;

    //Variables int
    public int saltosExtras;
    public int saltosExtrasRestantes;

    //Variables Vector3 y Vector2
    Vector3 vectorSalto;
    Vector3 velocidadDamp;
    Vector3 movimiento;
    Vector3 move;
    Vector2 inputMovimientoWASD;

    // Start is called before the first frame update
    void Start()
    {
        inercia = 0.1f;
        fuerzaGravedad = -9.81f;
        fuerzaSalto = 5;
        longitudRayo = 1.1f;
        saltosExtras = 1;
        velocidadBase = 12;
        velocidad = velocidadBase;
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownDash += Time.deltaTime;
        cooldownSlide += Time.deltaTime;
        cooldownSalto += Time.deltaTime;
    }
    private void FixedUpdate()
    {
        //Se encarga de recibir el input del teclado y calcular la velocidad de movimiento y añadir la inercia
        inputMovimientoWASD = playerInput.actions["Move"].ReadValue<Vector2>();
        move = transform.right * inputMovimientoWASD.x + transform.forward * inputMovimientoWASD.y;
        movimiento = Vector3.SmoothDamp(movimiento, move * velocidad, ref velocidadDamp, inercia);

        //Se encarga de mover al personaje
        characterController.Move(movimiento * Time.deltaTime);

        //Se encarga de añadir gravedad al personaje
        vectorSalto.y += fuerzaGravedad * Time.deltaTime;

        //Se encarga de hacer saltar al personaje
        characterController.Move(vectorSalto * Time.deltaTime);

        //Se encarga de mirar si el personaje esta colisionandio con "algo" para poder saltar
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

    //Se encarga de hacer el slide durante X segundos
    public void slide(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && colisionSuelo == true && cooldownSlide >= 2f)
        {
            velocidad = velocidadBase + 12;
            transform.localScale = new Vector3 (transform.localScale.x, 0.5f, transform.localScale.z);
            cooldownSlide = 0;
        }
        else
        {
            velocidad = velocidadBase;
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

    //Se encarga de hacer el dash pero solo de forma lateral y hacia atras NO se puede hacerlo de frente
    public void dash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && cooldownDash >= 2 && (inputMovimientoWASD.x >= 0.1 || inputMovimientoWASD.x <= -0.1 || inputMovimientoWASD.y <= 0))
        {
            velocidad = velocidadBase + 50;
            cooldownDash = 0;
        }
        else if (cooldownDash >= 0.1)
        {
            velocidad = velocidadBase;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 direccionGizmo = -transform.up * 1.1f;
        Gizmos.DrawRay(transform.position, direccionGizmo);
    }
}
