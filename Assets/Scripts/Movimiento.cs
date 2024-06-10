using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Movimiento : MonoBehaviour
{
    //Declaracion del Script de las armas para la recarga
    public CambioArmas cambioArmas;
    public Inyeccion scriptInyeccion;

    //Declaraciones
    PlayerInput playerInput;
    CharacterController characterController;

    //Variables float "Cooldowns"
    public bool cooldownSalto;
    public bool cooldownSlide;
    public bool cooldownDash;

    //Variables float "Velocidad"
    public float inercia;

    //Variables float "Salto"
    float longitudRayo;
    float fuerzaGravedad;

    //Variables bool
    bool colisionSuelo;

    //Variable bool que se encarga de si estas agachado no poder hacer ni el slide ni el dash
    public bool agachado;

    //Variable relacionada con los consumibles y las granadas
    public GameObject salidaConsumibles;
    public GameObject granadaPrefab;
    public GameObject granada;
    public float fuerzaLanzamiento;


    //Tengo que crear la tienda para poder seguir con esto y crear el cambio de granadas
    public bool granadaPilladaPEM;


    //Variables int
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
        longitudRayo = 1.1f;
        fuerzaLanzamiento = 25;
        cooldownDash = true;
        cooldownSalto = true;
        cooldownSlide = true;
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
        salidaConsumibles = GameObject.Find("SalidaConsumibles");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        //Se encarga de recibir el input del teclado y calcular la velocidad de movimiento y añadir la inercia
        inputMovimientoWASD = playerInput.actions["Move"].ReadValue<Vector2>();
        move = transform.right * inputMovimientoWASD.x + transform.forward * inputMovimientoWASD.y;
        movimiento = Vector3.SmoothDamp(movimiento, move * GameManager.Instance.velocidadActual, ref velocidadDamp, inercia);

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
            Invoke("coolDownSaltar", 0.1f);
            vectorSalto.y = GameManager.Instance.fuerzaSalto;
            cooldownSalto = false;
            colisionSuelo = false;
        }
        if (context.phase == InputActionPhase.Performed && cooldownSalto == true && saltosExtrasRestantes > 0)
        {
            Invoke("coolDownSaltar", 0.1f);
            vectorSalto.y = GameManager.Instance.fuerzaSalto;
            cooldownSalto = false;
            saltosExtrasRestantes -= 1;
        }
    }

    //Se encarga del cooldown del salto
    public void coolDownSaltar()
    {
        cooldownSalto = true;
    }

    //Se encarga de hacer el slide durante X segundos
    public void slide(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && colisionSuelo == true && cooldownSlide == true && agachado == false)
        {
            Invoke("coolDownSlide", 2);
            GameManager.Instance.velocidadActual += 10;
            characterController.height = 1;
            cooldownSlide = false;
        }
        else
        {
            GameManager.Instance.velocidadActual = GameManager.Instance.velocidadBase;
            characterController.height = 2;
        }
    }

    //Se encarga del cooldown del slide
    public void coolDownSlide()
    {
        cooldownSlide = true;
    }

    //Se encarga del agachado y la reduccion de velocidad correspondiente del personaje
    public void agachar(InputAction.CallbackContext context)
    {
        if (playerInput.actions["Agacharse"].IsPressed() && context.performed)
        {   
            GameManager.Instance.velocidadActual /= 2;
            agachado = true;
            characterController.height = 0.5f;
        }
        else if (context.canceled)
        {
            agachado = false;
            GameManager.Instance.velocidadActual = GameManager.Instance.velocidadBase;
            characterController.height = 2;
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
            saltosExtrasRestantes = GameManager.Instance.saltosExtrasBase;
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
        if (context.phase == InputActionPhase.Started && cooldownDash == true && agachado == false && (inputMovimientoWASD.x >= 0.1 || inputMovimientoWASD.x <= -0.1 || inputMovimientoWASD.y <= 0))
        {
            Invoke("coolDownDash", 1.5f);
            GameManager.Instance.velocidadActual +=  60;
            cooldownDash = false;
        }
        else
        {
            GameManager.Instance.velocidadActual = GameManager.Instance.velocidadBase;
        }
    }

    //Se encarga del cooldown del dash
    public void coolDownDash()
    {
        cooldownDash = true;
    }

    //Se encarga de empezar la recarga de las armas
    //Esta en este script ya que el InputSystem de Unity da bastantes fallos y se tuvo que mover a este script para el correcto funcionamiento
    public void Recarga(InputAction.CallbackContext context)
    {
        if (context.started && cambioArmas.armaActiva.GetComponent<ArmasDatos>().balasRestantes < cambioArmas.armaActiva.GetComponent<ArmasDatos>().cargador)
        {
            cambioArmas.armaActiva.GetComponent<ArmasDatos>().recargando();
        }
    }

    //Se encarga que al pulsar la tecla asignada se ejecute el script de la inyeccion
    public void usarInyeccion(InputAction.CallbackContext context)
    {
        if (context.performed && GameManager.Instance.inyeccionesRestantes > 0)
        {
            scriptInyeccion.EfectoInyeccion();
        }
    }

    //Se encarga de instanciar y lanzar la granada en la direccion que estas mirando
    public void lanzarGranada(InputAction.CallbackContext context)
    {
        if (context.performed && GameManager.Instance.consumiblesRestantes > 0)
        {
            GameManager.Instance.consumiblesRestantes--;
            granada = Instantiate(granadaPrefab, salidaConsumibles.transform.position, Quaternion.identity);
            granada.GetComponent<Rigidbody>().AddForce(salidaConsumibles.transform.forward * fuerzaLanzamiento, ForceMode.Impulse);
        }
    }

    //Al colisionar con los objetos que representan las habilidades se activan y tambien las armas se equipan.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Habilidad")
        {
            other.GetComponent<IHabilidadesManager>().ActivarHabilidad();
            GameManager.Instance.velocidadActual = GameManager.Instance.velocidadBase;
        }
        if (other.gameObject.tag == "Consumible")
        {
            other.GetComponent<IRecogerConsumible>().consumibleRecolectado();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 direccionGizmo = -transform.up * 1.1f;
        Gizmos.DrawRay(transform.position, direccionGizmo);
    }
}
