using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumibleGranada : MonoBehaviour, IRecogerConsumible
{
    public void consumibleRecolectado()
    {
        GameManager.Instance.AumentarNumConsumibles();
        Destroy(gameObject);
    }
}
