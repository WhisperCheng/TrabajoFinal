using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pluma : MonoBehaviour, IHabilidadesManager
{
    void IHabilidadesManager.ActivarHabilidad()
    {
        GameManager.Instance.AumentarSaltosExtras();
        Destroy(gameObject);
    }
}
