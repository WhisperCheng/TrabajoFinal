using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumibleInyeccion : MonoBehaviour, IRecogerConsumible
{
    public void consumibleRecolectado()
    {
        GameManager.Instance.AumentarNumInyeccion();
        Destroy(gameObject);
    }
}
