using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inyeccion : MonoBehaviour, IRecogerConsumible
{
    public void consumibleRecolectado()
    {
        GameManager.Instance.AumentarNumInyeccion();
        Destroy(gameObject);
    }
}
