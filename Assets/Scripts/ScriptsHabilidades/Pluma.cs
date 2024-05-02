using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pluma : MonoBehaviour, HabilidadesManager
{
    void HabilidadesManager.ActivarHabilidad()
    {
        GameManager.Instance.AumentarSaltosExtras();
        Destroy(gameObject);
    }
}
