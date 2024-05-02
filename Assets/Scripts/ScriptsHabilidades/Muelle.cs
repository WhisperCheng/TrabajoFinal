using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muelle : MonoBehaviour, HabilidadesManager
{
    void HabilidadesManager.ActivarHabilidad()
    {
        GameManager.Instance.AumentarFuerzaSalto();
        Destroy(gameObject);
    }
}
