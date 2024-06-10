using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muelle : MonoBehaviour, Ihabilidades
{
    public void habilidadRecogida()
    {
        GameManager.Instance.AumentarFuerzaSalto();
        Destroy(gameObject);
    }
}
