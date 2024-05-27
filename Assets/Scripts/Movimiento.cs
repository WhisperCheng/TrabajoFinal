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

    //Declaraciones
    PlayerInput playerInput;
    CharacterController characterController;

    //Variables float "Cooldowns"
    public float cooldownSalto;
    public float cooldownSlide;
    public float cooldownDash;

    //Variables float "Velocidad"
    public float inercia;

    //Variables float "Salto"
    float longitudRayo;
    float fuerzaGravedad;

    //Variables bool
    bool colisionSuelo;

    //Variable bool y float relacionado con la Inyeccion
    public bool efectoInyeccion;
    public float cooldownEfectoVelocidad;

    //Variable relacionada con los consumibles y las granadas
    public GameObject granadaPrefab;
    public GameObject granada;
    public GameObject salidaGranada;
    public float fuerzaLanzamiento;

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
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
        salidaGranada = GameObject.Find("SalidaConsumibles");
    }

    // Update is called once per frame
    void Update()
    {
        //Distintos cooldown del movimiento
        cooldownDash += Time.deltaTime;
        cooldownSlide += Time.deltaTime;
        cooldownSalto += Time.deltaTime;
        cooldownEfectoVelocidad += Time.deltaTime;
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
        efectoInyeccionTiempo();
    }

    //Se encarga de el salto del personaje
    public void saltar(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && colisionSuelo == true)
        {
            vectorSalto.y = GameManager.Instance.fuerzaSalto;
            cooldownSalto = 0;
            colisionSuelo = false;
        }
        if (context.phase == InputActionPhase.Performed && cooldownSalto >= 0.1f && saltosExtrasRestantes > 0)
        {
            vectorSalto.y = GameManager.Instance.fuerzaSalto;
            cooldownSalto = 0;
            saltosExtrasRestantes -= 1;
        }
    }

    //Se encarga de hacer el slide durante X segundos

    //Arreglar el escalado del personaje con el arma IMPORTANTE
    public void slide(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && colisionSuelo == true && cooldownSlide >= 2f)
        {
            GameManager.Instance.velocidadActual += 12;
            characterController.height = 1;
            cooldownSlide = 0;
        }
        else
        {
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
        if (context.phase == InputActionPhase.Started && cooldownDash >= 2 && (inputMovimientoWASD.x >= 0.1 || inputMovimientoWASD.x <= -0.1 || inputMovimientoWASD.y <= 0))
        {
            GameManager.Instance.velocidadActual +=  50;
            cooldownDash = 0;
        }
        else
        {
            GameManager.Instance.velocidadActual = GameManager.Instance.velocidadBase;
        }
    }

    //Se encarga de la recarga de las armas
    //Esta en este script ya que el InputSystem de Unity da bastantes fallos y se tuvo que mover a este script para el correcto funcionamiento
    public void Recarga(InputAction.CallbackContext context)
    {
        if (context.started && cambioArmas.armaActiva.GetComponent<ArmasDatos>().balasRestantes < cambioArmas.armaActiva.GetComponent<ArmasDatos>().cargador)
        {
            Debug.Log("Recargando");
            cambioArmas.armaActiva.GetComponent<ArmasDatos>().balasRestantes = cambioArmas.armaActiva.GetComponent<ArmasDatos>().cargador;
        }
    }

    //Se encarga de comprobar el escalado de los datos al usar la inyeccion "F"
    public void usarInyeccion(InputAction.CallbackContext context)
    {
        if (context.performed && GameManager.Instance.inyeccionesRestantes > 0)
        {
            GameManager.Instance.inyeccionesRestantes--;
            GameManager.Instance.cooldownRegeneracion = 5;
            GameManager.Instance.velocidadActual += 10;
            GameManager.Instance.regeneracionPerSegundoActual += 5;
            GameManager.Instance.cooldownRegeneracion = 5;
            efectoInyeccion = true;
            cooldownEfectoVelocidad = 0;
        }
    }

    //Se encarga de comprobar que el efecto de la inyeccion termine y todo vuelva a lo normal
    public void efectoInyeccionTiempo()
    {
        if (efectoInyeccion == true && cooldownEfectoVelocidad > 3)
        {
            GameManager.Instance.velocidadActual = GameManager.Instance.velocidadBase;
            GameManager.Instance.regeneracionPerSegundoActual = GameManager.Instance.regeneracionPerSegundoBase;
            efectoInyeccion = false;
        }
    }

    //Se encarga de instanciar y lanzar la granada en la direccion que estas mirando
    public void lanzarGranada(InputAction.CallbackContext context)
    {
        if (context.performed && GameManager.Instance.consumiblesRestantes > 0)
        {
            GameManager.Instance.consumiblesRestantes--;
            granada = Instantiate(granadaPrefab, salidaGranada.transform.position, Quaternion.identity);
            granada.GetComponent<Rigidbody>().AddForce(salidaGranada.transform.forward * fuerzaLanzamiento, ForceMode.Impulse);
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

        if (other.gameObject.tag == "Arma")
        {
            other.GetComponent<IRecogerArmas>().armaRecolectada();
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
