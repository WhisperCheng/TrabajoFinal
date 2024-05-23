using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandolera : MonoBehaviour, IHabilidadesManager
{
    void IHabilidadesManager.ActivarHabilidad()
    {
        GameManager.Instance.AumentarCapacidadConsumibles();
        Destroy(gameObject);
    }
}
