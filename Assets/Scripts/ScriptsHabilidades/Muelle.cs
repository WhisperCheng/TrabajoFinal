using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muelle : MonoBehaviour, IHabilidadesManager
{
    void IHabilidadesManager.ActivarHabilidad()
    {
        GameManager.Instance.AumentarFuerzaSalto();
        Destroy(gameObject);
    }
}
