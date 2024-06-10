using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandolera : MonoBehaviour, Ihabilidades
{
    public void habilidadRecogida()
    {
        GameManager.Instance.AumentarCapacidadConsumibles();
        Destroy(gameObject);
    }
}
