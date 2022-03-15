using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototipagemAplicarDebuff : MonoBehaviour
{
    private void Start()
    {
        DebuffPlayer.Instance.objetosAtravesaveis.Add(gameObject);
    }
}
