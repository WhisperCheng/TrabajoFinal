using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacaArmadura : MonoBehaviour, Ihabilidades
{
    public void habilidadRecogida()
    {
        GameManager.Instance.AumentarReduccionDaño();
        Destroy(gameObject);
    }
}
