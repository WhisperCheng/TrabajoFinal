using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacaArmadura : MonoBehaviour, HabilidadesManager
{
    void HabilidadesManager.ActivarHabilidad()
    {
        GameManager.Instance.AumentarReduccionDa�o();
        Destroy(gameObject);
    }
}
