using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento : MonoBehaviour
{
    //declaraciones
    Rigidbody cuerpo;
    PlayerInput playerInput;

    //variables
    public float velocidadMovimiento;
    bool colisionSuelo;
    float cooldownSalto;
    public int saltosExtras;
    public int saltosExtrasRestantes;
    float longitudRayo;

    // Start is called before the first frame update
    void Start()
    {
        longitudRayo = 1.1f;
        saltosExtras = 1;
        velocidadMovimiento = 12;
        cuerpo = GetComponent<Rigidbody>(); 
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownSalto += Time.deltaTime;
    }
    private void FixedUpdate()
    {
        //Movimiento del personaje
        Vector2 inputMovimiento = playerInput.actions["Move"].ReadValue<Vector2>();
        cuerpo.velocity = new Vector3(inputMovimiento.x * velocidadMovimiento, cuerpo.velocity.y, inputMovimiento.y * velocidadMovimiento);

        rayoSalto();
    }

    public void saltar(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && colisionSuelo == true)
        {
            Debug.Log("Saltando");
            cuerpo.AddForce(Vector2.up * 8, ForceMode.Impulse);
            cooldownSalto = 0;
            colisionSuelo = false;
        }
        if (context.phase == InputActionPhase.Performed && cooldownSalto >= 0.1f && saltosExtrasRestantes > 0)
        {
            Debug.Log("DobleSalto");
            cuerpo.AddForce(Vector2.up * 8, ForceMode.Impulse);
            cooldownSalto = 0;
            saltosExtrasRestantes -= 1;
        }
    }

    public void rayoSalto()
    {
        Vector3 origen = transform.position;

        Vector3 direccion = -transform.up;

        RaycastHit golpeSuelo;

        if (Physics.Raycast(origen, direccion, out golpeSuelo, longitudRayo)) 
        {
            colisionSuelo = true;
            saltosExtrasRestantes = saltosExtras;
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
