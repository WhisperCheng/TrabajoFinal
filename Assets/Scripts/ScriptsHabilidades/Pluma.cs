using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pluma : MonoBehaviour, Ihabilidades
{
    public void habilidadRecogida()
    {
        GameManager.Instance.AumentarSaltosExtras();
        Destroy(gameObject);
    }
}
