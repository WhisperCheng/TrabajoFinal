using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BebidaEnergetica : MonoBehaviour, IHabilidadesManager
{
    void IHabilidadesManager.ActivarHabilidad()
    {
        GameManager.Instance.AumentarVelocidad();
        Destroy(gameObject);
    }
}
