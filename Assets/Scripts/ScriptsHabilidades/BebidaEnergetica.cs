using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BebidaEnergetica : MonoBehaviour, Ihabilidades
{
    public void habilidadRecogida()
    {
        GameManager.Instance.AumentarVelocidad();
        Destroy(gameObject);
    }
}
