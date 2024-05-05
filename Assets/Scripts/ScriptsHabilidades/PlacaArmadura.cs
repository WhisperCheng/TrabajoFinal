using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacaArmadura : MonoBehaviour, IHabilidadesManager
{
    void IHabilidadesManager.ActivarHabilidad()
    {
        GameManager.Instance.AumentarReduccionDa�o();
        Destroy(gameObject);
    }
}
